using System;
using System.Windows;

namespace T9
{
    public partial class AddWordWindow : Window
    {
        public AddWordWindow(Action<string> onAccept)
        {
            InitializeComponent();
            inputField.Focus();
            acceptBtn.Click += (s, e) =>
            {
                onAccept?.Invoke(inputField.Text.ToLower());
                Close();
            };
            cancelBtn.Click += (s, e) => Close();
        }
    }
}
