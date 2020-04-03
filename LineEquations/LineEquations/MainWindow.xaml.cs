using System;
using System.IO;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;

namespace LineEquations
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void OpenCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void OpenCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Format (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string text;
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                    text = reader.ReadToEnd();
                InputView.Text = text;
                int[,] source = ParseFile(text);
                DrawSource(source);
                float[] result = Calculate(source);
                DrawResult(result);
            }

            void DrawSource(int[,] matrix)
            {
                string tempStr = string.Empty;
                for (int i = 0, j; i < matrix.GetLength(0); i++)
                {
                    for (j = 0; j < matrix.GetLength(1); j++)
                        tempStr += $"{matrix[i, j]}\t";
                    tempStr += '\n';
                }
                ParsedView.Text = tempStr;
            }

            void DrawResult(float[] result)
            {
                if (result == null)
                {
                    OutputView.Text = "null";
                    return;
                }
                string tempStr = string.Empty;
                for (int i = 0; i < result.Length; i++)
                    tempStr += $"x{i + 1}: {result[i]}\n";
                OutputView.Text = tempStr;
            }
        }

        private void SaveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e) => e.CanExecute = true;

        private void SaveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text Format (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, OutputView.Text);
        }

        private int[,] ParseFile(string text)
        {
            text = text.Replace(" ", "").Replace("-", "+-").Replace("=+", "=");
            string[] lines = text.Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int maxWidth = 0;
            string[][] args = new string[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                args[i] = lines[i].Split(new char[2] { '+', '=' }, StringSplitOptions.RemoveEmptyEntries);
                maxWidth = Math.Max(maxWidth, args[i].Length);
                if (int.TryParse(args[i][args[i].Length - 2].Split('x')[1], out int xMaxIndex))
                    maxWidth = Math.Max(maxWidth, xMaxIndex + 1);
            }
            int[,] matrix = new int[lines.Length, maxWidth];
            string[] tempArg;
            int tempArgValue;
            for (int line = 0, argIndex; line < lines.Length; line++)
            {
                for (argIndex = 0; argIndex < args[line].Length - 1; argIndex++)
                {
                    tempArg = args[line][argIndex].Split('x');
                    if (tempArg[0] == "-")
                        tempArgValue = -1;
                    else if (int.TryParse(tempArg[0], out tempArgValue) == false)
                        tempArgValue = 1;
                    matrix[line, int.Parse(tempArg[1]) - 1] = tempArgValue;
                }
                matrix[line, maxWidth - 1] = int.Parse(args[line][args[line].Length - 1]);
            }
            return matrix;
        }

        private float[] Calculate(int[,] source)
        {
            int range = source.GetLength(0);
            Matrix matrix = new Matrix(range);
            matrix.Fill(source);
            float detA = Matrix.Determinant(matrix);
            if (detA == 0f)
                return null;
            float[] result = new float[range];
            Matrix temp = new Matrix(range + 1);
            temp.Fill(source);
            for (int i = 0; i < range; i++)
            {
                Matrix xMatrix = new Matrix(range);
                xMatrix.Fill(source);
                xMatrix.CopyColumn(temp, range, i);
                result[i] = Matrix.Determinant(xMatrix) / detA;
            }
            return result;
        }
    }
}
