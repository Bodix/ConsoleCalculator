using System;

namespace ConsoleCalculator
{
    class Matrix
    {
        public static double[,] AddMatrixToMatrix(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
                throw new ArgumentException("Ошибка: Размерности матриц не совпадают.");
            double[,] result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    result[row, col] = matrix1[row, col] + matrix2[row, col];
            return result;
        }
        public static double[,] DeductMatrixFromMatrix(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(0) != matrix2.GetLength(0) || matrix1.GetLength(1) != matrix2.GetLength(1))
                throw new ArgumentException("Ошибка: Размерности матриц не совпадают.");
            double[,] result = new double[matrix1.GetLength(0), matrix1.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    result[row, col] = matrix1[row, col] - matrix2[row, col];
            return result;
        }
        public static double[,] MultiplyMatrixToNumber(double[,] matrix, double number)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    result[row, col] = matrix[row, col] * number;
            return result;
        }
        public static double[] MultiplyMatrixToVector(double[,] matrix, double[] vector)
        {
            if (matrix.GetLength(1) != vector.GetLength(0))
                throw new ArgumentException("Ошибка: Количество столбцов матрицы не равно размеру вектора.");
            double[] result = new double[matrix.GetLength(0)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = Vector.MultiplyScalar(GetRow(matrix, i), vector);
            }
            return result;
        }
        public static double[,] MultiplyMatrixToMatrix(double[,] matrix1, double[,] matrix2)
        {
            if (matrix1.GetLength(1) != matrix2.GetLength(0))
                throw new ArgumentException("Ошибка: Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            double[,] result = new double[matrix1.GetLength(0), matrix2.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    result[row, col] = Vector.MultiplyScalar(GetRow(matrix1, row), GetColumn(matrix2, col));
            return result;
        }
        private static double[] GetRow(double[,] matrix, int rowNumber)
        {
            double[] result = new double[matrix.GetLength(1)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = matrix[rowNumber, i];
            }
            return result;
        }
        private static double[] GetColumn(double[,] matrix, int columnNumber)
        {
            double[] result = new double[matrix.GetLength(0)];
            for (int i = 0; i < result.GetLength(0); i++)
            {
                result[i] = matrix[i, columnNumber];
            }
            return result;
        }
        public static double[,] GetTransposeMatrix(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(1), matrix.GetLength(0)];
            for (int row = 0; row < result.GetLength(1); row++)
                for (int col = 0; col < result.GetLength(0); col++)
                    result[row, col] = matrix[col, row];
            return result;
        }
        private static double[,] RemoveRow(double[,] matrix, int rowToRemove)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    if (row >= rowToRemove)
                    {
                        result[row, col] = matrix[row + 1, col];
                    }
                    else
                    {
                        result[row, col] = matrix[row, col];
                    }
            return result;
        }
        private static double[,] RemoveColumn(double[,] matrix, int columnToRemove)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1) - 1];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    if (col >= columnToRemove)
                    {
                        result[row, col] = matrix[row, col + 1];
                    }
                    else
                    {
                        result[row, col] = matrix[row, col];
                    }
            return result;
        }
        private static double[,] GetSubMatrix(double[,] matrix, int rowToRemove, int colToRemove)
        {
            double[,] result = new double[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            result = RemoveRow(RemoveColumn(matrix, colToRemove), rowToRemove);
            return result;
        }
        public static double GetDeterminant(double[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
                throw new ArgumentException("Ошибка: Матрица не квадратная.");
            double result = 0;
            if (matrix.GetLength(0) == 1 && matrix.GetLength(1) == 1)
                result = matrix[0, 0];
            else
                for (int col = 0; col < matrix.GetLength(1); col++)
                    result += Math.Pow(-1, col) * matrix[0, col] * GetDeterminant(GetSubMatrix(matrix, 0, col));
            return result;
        }
        private static double[,] GetMinorMatrix(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    result[row, col] = GetDeterminant(GetSubMatrix(matrix, row, col));
            return result;
        }
        private static double[,] GetCofactorMatrix(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                    result[row, col] = Math.Pow(-1, row + col) * matrix[row, col];
            return result;
        }
        public static double[,] GetInvertMatrix(double[,] matrix)
        {
            return MultiplyMatrixToNumber(GetTransposeMatrix(GetCofactorMatrix(GetMinorMatrix(matrix))),
                1 / GetDeterminant(matrix));
        }
    }
}
