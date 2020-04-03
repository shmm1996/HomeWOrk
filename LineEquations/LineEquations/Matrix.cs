using System;

namespace LineEquations
{
    public class Matrix
    {

        private int[,] values;

        public int this[int line, int column] 
        {
            get => values[line, column];
            set => values[line, column] = value;
        }

        public int Range { get; private set; }

        public Matrix(int range)
        {
            values = new int[range, range];
            Range = range;
        }

        public void Fill(int[,] values)
        {
            int valuesLines = values.GetLength(0);
            int valuesColumns = values.GetLength(1);

            for (int line = 0, column; line < Range && line < valuesLines; line++)
            {

                for(column = 0; column < Range && column < valuesColumns; column++)
                {
                    this[line, column] = values[line, column];
                }
            }
        }

        public Matrix Minor(int cutLine, int cutColumn)
        {
            Matrix minor = new Matrix(Range - 1);

            int minorLine = 0;
            int minorColumn = 0;

            for(int line = 0, column; line < Range; line++)
            {
                if (line == cutLine)
                    continue;

                for(column = 0; column < Range; column++)
                {
                    if (column == cutColumn)
                        continue;

                    minor[minorLine, minorColumn] = this[line, column];

                    minorColumn++;
                }

                minorLine++;

                minorColumn = 0;
            }

            return minor;
        }

        public void CopyColumn(Matrix sourceMatrix, int sourceColumn, int targetColumn)
        {
            for (int line = 0; line < Range && line < sourceMatrix.Range; line++)
                this[line, targetColumn] = sourceMatrix[line, sourceColumn];
        }

        public static float Determinant(Matrix matrix)
        {
            float result = 0;

            if(matrix.Range == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                Matrix minor;
                int k = 1;
                for(int column = 0; column < matrix.Range; column++)
                {
                    minor = matrix.Minor(1, column);

                    result += matrix[1, column] * k * Determinant(minor);

                    k *= -1;
                }
            }

            return result;
        }
    }
}
