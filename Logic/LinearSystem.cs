using System;

namespace ConsoleCalculator
{
    class LinearSystem
    {
        public static double eps = 1e-5;

        // x = A^-1 * b
        public static double[] SolveLSByInvertMatrix(double[,] a, double[] b)
        {
            return Matrix.MultiplyMatrixToVector(Matrix.GetInvertMatrix(a), b);
        }
        // x = Ax + b
        public static double[] SolveLSBySimpleIterations(double [,] a, double [] b)
        {
            if (!IsMatrixConvergense(a))
                throw new Exception("Ошибка: Матрица не сходится.");
            double[] prevResult = new double[a.GetLength(1)];
            double[] nextResult = Vector.AddVectorToVector(Matrix.MultiplyMatrixToVector(a, prevResult), b);
            while (!IsIterationsFinished(prevResult, nextResult))
            {
                prevResult = nextResult;
                nextResult = Vector.AddVectorToVector(Matrix.MultiplyMatrixToVector(a, prevResult), b);
            }
            return nextResult;
        }
        private static bool IsMatrixConvergense(double[,] matrix)
        {
            double sum = 0;
            for (int row = 0; row < matrix.GetLength(0); row++)
                for (int col = 0; col < matrix.GetLength(1); col++)
                    sum += matrix[row, col] * matrix[row, col];
            sum = Math.Sqrt(sum);
            if (sum < 1)
                return true;
            else return false;
        }
        private static bool IsIterationsFinished(double[] previous, double[] next)
        {
            double[] difference = new double[previous.GetLength(0)];
            for (int i = 0; i < difference.GetLength(0); i++)
            {
                difference[i] = Math.Abs(previous[i] - next[i]);
                if (difference[i] > eps) return false;
            }
            return true;
        }
    }
}
