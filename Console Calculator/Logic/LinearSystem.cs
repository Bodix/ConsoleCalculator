using System;

namespace ConsoleCalculator
{
    class LinearSystem
    {
        public static double eps = 1e-5;
        
        public static double[] SolveByInvertMatrix(double[,] a, double[] b)
        {
            return Matrix.MultiplyMatrixToVector(Matrix.GetInvertMatrix(a), b);
        }
        public static double[] SolveByIteration(double[,] matrix)
        {
            matrix = ConvertToIterationForm(matrix);
            double[,] a = new double[matrix.GetLength(0), matrix.GetLength(1) - 1];
            double[] b = new double[matrix.GetLength(0)];
            for (int row = 0; row < a.GetLength(0); row++)
                for (int col = 0; col < a.GetLength(1) - 1; col++)
                {
                    a[row, col] = matrix[row, col];
                }
            if (!IsMatrixConvergense(a))
                throw new Exception("Матрица не сходится");
            for (int i = 0; i < b.Length; i++)
            {
                b[i] = matrix[i, matrix.GetLength(1) - 1];
            }
            double[] prevResult = new double[a.GetLength(1)];
            double[] nextResult = Vector.AddVectorToVector(Matrix.MultiplyMatrixToVector(a, prevResult), b);
            while (!IsIterationsFinished(prevResult, nextResult))
            {
                prevResult = nextResult;
                nextResult = Vector.AddVectorToVector(Matrix.MultiplyMatrixToVector(a, prevResult), b);
            }
            return nextResult;
        }
        public static double[] SolveByGauss(double[,] matrix)
        {
            if (matrix.GetLength(0) > matrix.GetLength(1) - 1)
                throw new Exception("СЛАУ недоопределена");
            double[] result = new double[matrix.GetLength(0)];
            double coeff;
            for (int k = 1; k < matrix.GetLength(0); k++)
                for (int j = k; j < matrix.GetLength(0); j++)
                {
                    coeff = matrix[j, k - 1] / matrix[k - 1, k - 1];
                    for (int i = 0; i < matrix.GetLength(1); i++)
                        matrix[j, i] = matrix[j, i] - coeff * matrix[k - 1, i];
                }
            for (int i = matrix.GetLength(0) - 1; i >= 0; i--)
            {
                result[i] = matrix[i, matrix.GetLength(1) - 1];
                for (int j = i + 1; j < matrix.GetLength(0); j++)
                {
                    result[i] -= matrix[i, j] * result[j];
                }
                result[i] = result[i] / matrix[i, i];
            }
            return result;
        }
        public static double[,] ConvertToExtendedMatrix(double[,] a, double[] b)
        {
            if (a.GetLength(0) != b.GetLength(0))
                throw new Exception("Невозможно привести матрицу и вектор к расширенной матрице");
            double[,] result = new double[a.GetLength(0), a.GetLength(1) + 1];
            for (int row = 0; row < a.GetLength(0); row++)
                for (int col = 0; col < a.GetLength(1); col++)
                {
                    result[row, col] = a[row, col];
                }
            for (int i = 0; i < b.Length; i++)
            {
                result[i, a.GetLength(1)] = b[i];
            }
            return result;
        }
        private static double[,] ConvertToIterationForm(double[,] matrix)
        {
            double[,] result = new double[matrix.GetLength(0), matrix.GetLength(1)];
            for (int row = 0; row < result.GetLength(0); row++)
                for (int col = 0; col < result.GetLength(1); col++)
                {
                    if (col == result.GetLength(1) - 1)
                        result[row, col] = matrix[row, col] / matrix[row, row];
                    else
                        result[row, col] = -matrix[row, col] / matrix[row, row];
                    if (row == col)
                        result[row, col] = 0;
                }
            return result;
        }
        private static bool IsMatrixConvergense(double[,] matrix)
        {
            double c = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
                for (int col = 0; col < matrix.GetLength(1); col++)
                    c += Math.Pow(matrix[row, col], 2);
            c = Math.Sqrt(c);
            if (c >= 1)
                return false;
            else return true;
        }
        private static bool IsIterationsFinished(double[] previous, double[] next)
        {
            double[] difference = new double[previous.Length];
            for (int i = 0; i < difference.Length; i++)
            {
                difference[i] = Math.Abs(previous[i] - next[i]);
                if (difference[i] > eps) return false;
            }
            return true;
        }
    }
}
