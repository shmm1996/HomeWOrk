using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace T9
{
    public partial class MainWindow : Window
    {
        private int _t9StartAt;
        private readonly IList<char[]> _t9Letters;
        private IList<string> _dictionary;

        public MainWindow()
        {
            InitializeComponent();
            _t9Letters = new List<char[]>();
            Init();
        }

        private void Init()
        {
            LoadDictionary();
            LoadKeyboard();
            addBtn.Click += (s, e) => ShowAddWordWindow();
            spaceBtn.Click += (s, e) => InputSpace();
            backspaceBtn.Click += (s, e) => InputBackspace();
        }

        private void LoadDictionary()
        {
            var openFileDialog = new OpenFileDialog
                {Filter = "Text Format (*.txt)|*.txt|All files (*.*)|*.*"};
            while (true)
                if (openFileDialog.ShowDialog() == true)
                {
                    string text;
                    using (var reader = new StreamReader(openFileDialog.FileName))
                        text = reader.ReadToEndAsync().Result;
                    _dictionary = text.Split(new[] {'\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).ToList();
                    if (_dictionary.Count != 0)
                        return;
                }
        }

        private void LoadKeyboard()
        {
            var keyboardLetters = new[]
            {
                null,
                null,
                new[] {'A', 'B', 'C'},
                new[] {'D', 'E', 'F'},
                new[] {'G', 'H', 'I'},
                new[] {'J', 'K', 'L'},
                new[] {'M', 'N', 'O'},
                new[] {'P', 'Q', 'R', 'S'},
                new[] {'T', 'U', 'V'},
                new[] {'W', 'X', 'Y', 'Z'}
            };
            var i = -1;
            foreach (var keyLetters in keyboardLetters)
            {
                i++;
                if (keyLetters == null || keyLetters.Length == 0) continue;
                if (!(FindName($"n{i}Btn") is Button btn)) continue;
                btn.Content += keyLetters.Aggregate(" ", (current, c) => current + c);
                btn.Click += (s, e) => InputLettersKey(keyLetters);
            }
        }

        private void ShowAddWordWindow()
        {
            var addWordWindow = new AddWordWindow(x => _dictionary.Add(x));
            Closed += (s, e) => addWordWindow.Close();
            addWordWindow.Closed += (s, e) => IsEnabled = true;
            addWordWindow.Show();
            IsEnabled = false;
        }

        private void InputLettersKey(params char[] letters)
        {
            _t9Letters.Add(letters);
            UpdateT9();
        }

        private void InputSpace()
        {
            _t9Letters.Clear();
            //textField.Text = textField.Text.ToLower() + " "; // <-- Быстро
            textField.Text = textField.Text.Substring(0, _t9StartAt) + t9Dropdown.SelectedValue + " "; // <-- Медленно
            _t9StartAt = textField.Text.Length;
            t9Dropdown.ItemsSource = null;
        }

        private void InputBackspace()
        {
            if (_t9Letters.Count == 0)
                return;
            _t9Letters.RemoveAt(_t9Letters.Count - 1);
            UpdateT9();
        }

        private void UpdateT9()
        {
            IEnumerable<string> words = null;
            var t9Thread = new Thread(() =>
            {
                words = T9Word(_t9Letters);
                Thread.CurrentThread.Abort();
            });
            t9Thread.Start();
            while (t9Thread.IsAlive)
                Thread.Sleep(1);
            t9Dropdown.ItemsSource = words; // <-- Медленно
            t9Dropdown.SelectedIndex = 0;
            UpdateTextField(words.FirstOrDefault());
        }

        private void UpdateTextField(string word)
        {
            if (word == null)
            {
                ShowAddWordWindow();
                textField.Text = textField.Text.Substring(0, _t9StartAt);
                _t9Letters.Clear();
                return;
            }

            textField.Text = textField.Text.Substring(0, _t9StartAt)
                             + word.Substring(0, _t9Letters.Count).ToUpper()
                             + word.Substring(_t9Letters.Count, word.Length - _t9Letters.Count);
        }

        private IEnumerable<string> T9Word(IList<char[]> t9Letters)
        {
            IEnumerable<string> result = _dictionary;
            for (var i = 0; i < t9Letters.Count; i++)
            {
                var i1 = i;
                result = from word in result
                    where Contain(word, i1)
                    select word;
            }

            return result;

            bool Contain(string word, int letterPosition) => word.Length > letterPosition && t9Letters[letterPosition]
                .Any(letter =>
                    word[letterPosition].ToString()
                        .Equals(letter.ToString(),
                            StringComparison
                                .CurrentCultureIgnoreCase));
        }
    }
}