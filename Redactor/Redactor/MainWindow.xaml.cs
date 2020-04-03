using System;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace Redactor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Text_field.Focus();
        }
        private bool CheckTextCanExecute => Text_field.SelectionLength != 0;
        private void CutCommand_CanExecute(object s, CanExecuteRoutedEventArgs e) => e.CanExecute = CheckTextCanExecute;
        private void CutCommand_Execute(object sender, ExecutedRoutedEventArgs e) => Text_field.Cut();
        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e) => Text_field.Paste();
        private void CopyCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)=> e.CanExecute = CheckTextCanExecute;
        private void CopyCommand_Executed(object sender, ExecutedRoutedEventArgs e) => Text_field.Copy();
        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Format (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, Text_field.Text);
        }

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Format (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    Text_field.Text = reader.ReadToEnd();
        }

        private void CloseCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e) => Environment.Exit(0);
    }
}
