using System;
using System.Linq;

namespace ConsoleCalculator
{
    class View
    {
        public static void Calculate()
        {
            Console.WriteLine("\nВведите команду:");
            Console.Write(">");
            string command = Console.ReadLine().ToString();
            if (command == "/vector")
            {
                Console.WriteLine("Введите действие (v+v, v-v, v*n, vLen, v*v):");
                int vectorSize;
                double[] v1, v2;
                switch (Console.ReadLine())
                {
                    case "v+v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        ShowVector(Matrix.AddVectorToVector(v1, v2));
                        break;
                    case "v-v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        ShowVector(Matrix.DeductVectorFromVector(v1, v2));
                        break;
                    case "v*n":
                        v1 = GetVector();
                        Console.Write("Число: ");
                        double n = Convert.ToDouble(Console.ReadLine());
                        ShowVector(Matrix.MultiplyVectorToNumber(v1, n));
                        break;
                    case "vLen":
                        v1 = GetVector();
                        Console.WriteLine("Результат: " + Matrix.GetVectorLength(v1));
                        Calculate();
                        break;
                    case "v*v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        Console.WriteLine("Результат: " + Matrix.MultiplyScalar(v1, v2));
                        Calculate();
                        break;
                }
            }
            if (command == "/matrix")
            {
                Console.WriteLine("Введите действие (m+m, m*n, mTrans, m*m, mDet, mInv):");
                double[,] m1, m2;
                switch (Console.ReadLine())
                {
                    case "m+m":
                        m1 = GetMatrix(1);
                        m2 = GetMatrix(2);
                        try
                        {
                            ShowMatrix(Matrix.AddMatrixToMatrix(m1, m2));
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                            Calculate();
                        }
                        break;
                    case "m*n":
                        m1 = GetMatrix();
                        Console.Write("Число: ");
                        double n = Convert.ToDouble(Console.ReadLine());
                        ShowMatrix(Matrix.MultiplyMatrixToNumber(m1, n));
                        break;
                    case "mTrans":
                        m1 = GetMatrix();
                        ShowMatrix(Matrix.TransposeMatrix(m1));
                        break;
                    case "m*m":
                        m1 = GetMatrix(1);
                        m2 = GetMatrix(2);
                        try
                        {
                            ShowMatrix(Matrix.MultiplyMatrixToMatrix(m1, m2));
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                            Calculate();
                        }
                        break;
                    case "mDet":
                        m1 = GetMatrix();
                        try
                        {
                        Console.WriteLine("Результат: " + Matrix.GetDeterminant(m1));
                        Calculate();
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                            Calculate();
                        }
                        break;
                    case "mInv":
                        m1 = GetMatrix();
                        ShowMatrix(Matrix.InvertMatrix(m1));
                        break;
                }
            }
            if (command == "/help")
            {
                Console.WriteLine("Список команд:\n/vector - операции с векторами\n/matrix - операции с матрицами\n/clear - очистить консоль");
                Calculate();
            }
            if (command == "/clear")
            {
                Console.Clear();
                Calculate();
            }
            else
            {
                Console.WriteLine("Команда не найдена (/help).");
                Calculate();
            }
        }

        private static int GetVectorSize()
        {
            Console.Write("Размер: ");
            return Convert.ToInt16(Console.ReadLine());
        }
        private static double[] GetVector()
        {
            double[] vector = new double[GetVectorSize()];
            Console.WriteLine("Вектор:");
            for (int i = 0; i < vector.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Console.Write((i + 1) + ": ");
                        vector[i] = Convert.ToDouble(Console.ReadLine());
                        flag = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Некорректное значение, попробуйте снова.");
                    }
                }
            }
            return vector;
        }
        private static double[] GetVector(int size, int index)
        {
            double[] vector = new double[size];
            Console.WriteLine("Вектор " + index + ":");
            for (int i = 0; i < vector.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Console.Write((i + 1) + ": ");
                        vector[i] = Convert.ToDouble(Console.ReadLine());
                        flag = false;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Некорректное значение, попробуйте снова.");
                    }
                }
            }
            return vector;
        }
        private static void ShowVector(double[] vector)
        {
            Console.WriteLine("Результат: ");
            for (int i = 0; i < vector.Length; i++)
                Console.Write("{0:F3} ", vector[i]);
            Console.WriteLine();
            Calculate();
        }

        private static double[,] GetMatrix()
        {
            Console.WriteLine("Матрица:");
            Console.Write("Кол-во строк: ");
            int rows = Convert.ToInt16(Console.ReadLine());
            Console.Write("Кол-во столбцов: ");
            int cols = Convert.ToInt16(Console.ReadLine());
            double[,] matrix = new double[rows, cols];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                string s = Console.ReadLine();
                int j = 0;
                foreach (int v in s.Split(' ').Select(v => Convert.ToInt32(v)))
                {
                    matrix[i, j++] = v;
                }
            }
            return matrix;
        }
        private static double[,] GetMatrix(int index)
        {
            Console.WriteLine("Матрица " + index + ":");
            Console.Write("Кол-во строк: ");
            int rows = Convert.ToInt16(Console.ReadLine());
            Console.Write("Кол-во столбцов: ");
            int cols = Convert.ToInt16(Console.ReadLine());
            double[,] matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                string s = Console.ReadLine();
                int j = 0;
                foreach (int v in s.Split(' ').Select(v => Convert.ToInt32(v)))
                {
                    matrix[i, j++] = v;
                }
            }
            return matrix;
        }
        private static void ShowMatrix(double[,] matrix)
        {
            Console.WriteLine("Результат: ");
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    Console.Write("{0:F3}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Calculate();
        }
    }
}
