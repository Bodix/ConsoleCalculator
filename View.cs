using System;
using System.Linq;

namespace ConsoleCalculator
{
    class View
    {
        public static void Calculate()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nВведите команду:");
            Console.Write("> ");
            string command = GetInput().ToString();
            if (command == "/vector")
            {
                Console.WriteLine(
@"Доступные действия:
v+v - сложение векторов
v-v - вычитание векторов
v*n - умножение вектора на число
vLen - длинна вектора
v*v - скалярное произведение векторов");
                Console.Write("Действие: ");
                int vectorSize;
                double[] v1, v2;
                switch (GetInput())
                {
                    case "v+v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        ShowVector(Vector.AddVectorToVector(v1, v2));
                        break;
                    case "v-v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        ShowVector(Vector.DeductVectorFromVector(v1, v2));
                        break;
                    case "v*n":
                        v1 = GetVector();
                        Console.Write("Число: ");
                        double n = Convert.ToDouble(GetInput());
                        ShowVector(Vector.MultiplyVectorToNumber(v1, n));
                        break;
                    case "vLen":
                        v1 = GetVector();
                        Console.WriteLine("Результат: " + Vector.GetVectorLength(v1));
                        Calculate();
                        break;
                    case "v*v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        Console.WriteLine("Результат: " + Vector.MultiplyScalar(v1, v2));
                        Calculate();
                        break;
                }
            }
            if (command == "/matrix")
            {
                Console.WriteLine(
@"Доступные действия:
m+m - сложение матриц
m-m - вычитание матриц
m*n - умножение матрицы на число
m*v - умножение матрицы на вектор
m*m - умножение матрицы на матрицу
mTrans - транспонирование матрицы
mDet - определитель матрицы
mInv - обратная матрица");
                Console.Write("Действие: ");
                double[,] m1, m2;
                try
                {
                    switch (GetInput())
                    {
                        case "m+m":
                            m1 = GetMatrix(1);
                            m2 = GetMatrix(2);
                            ShowMatrix(Matrix.AddMatrixToMatrix(m1, m2));
                            break;
                        case "m-m":
                            m1 = GetMatrix(1);
                            m2 = GetMatrix(2);
                            ShowMatrix(Matrix.DeductMatrixFromMatrix(m1, m2));
                            break;
                        case "m*n":
                            m1 = GetMatrix();
                            Console.Write("Число: ");
                            double n = Convert.ToDouble(GetInput());
                            ShowMatrix(Matrix.MultiplyMatrixToNumber(m1, n));
                            break;
                        case "m*v":
                            m1 = GetMatrix();
                            double[] v = GetVector();
                            ShowVector(Matrix.MultiplyMatrixToVector(m1, v));
                            break;
                        case "m*m":
                            m1 = GetMatrix(1);
                            m2 = GetMatrix(2);
                            ShowMatrix(Matrix.MultiplyMatrixToMatrix(m1, m2));
                            break;
                        case "mTrans":
                            m1 = GetMatrix();
                            ShowMatrix(Matrix.GetTransposeMatrix(m1));
                            break;
                        case "mDet":
                            m1 = GetMatrix();
                            Console.WriteLine("Результат: " + Matrix.GetDeterminant(m1));
                            Calculate();
                            break;
                        case "mInv":
                            m1 = GetMatrix();
                            ShowMatrix(Matrix.GetInvertMatrix(m1));
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    ShowException(e.Message);
                    Calculate();
                }
            }
            if (command == "/system")
            {
                Console.WriteLine(
@"Доступные действия:
byInv - решить СЛАУ методом обратной матрицы
byIter - решить СЛАУ методом простых итераций");
                Console.Write("Действие: ");
                double[,] a;
                double[] b;
                try
                {
                    switch (GetInput())
                    {
                        case "byInv":
                            a = GetMatrix();
                            b = GetVector();
                            ShowVector(LinearSystem.SolveLSByInvertMatrix(a, b));
                            break;
                        case "byIter":
                            a = GetMatrix();
                            b = GetVector();
                            ShowVector(LinearSystem.SolveLSBySimpleIterations(a, b));
                            break;
                    }
                }
                catch (ArgumentException e)
                {
                    ShowException(e.Message);
                    Calculate();
                }
            }
            if (command == "/help")
            {
                Console.WriteLine("Список команд:\n/vector - операции с векторами\n/matrix - операции с матрицами\n/system - операции с системами линейных алгебраических уравненй (СЛАУ)\n/clear - очистить консоль");
                Calculate();
            }
            if (command == "/clear")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
                Calculate();
            }
            else
            {
                Console.WriteLine("Команда не найдена (/help).");
                Calculate();
            }
        }
        private static string GetInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            return input;
        }
        private static void ShowException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        #region Vector input/output.
        private static int GetVectorSize()
        {
            int size = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Размер: ");
                    size = Int16.Parse(GetInput());
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException("Некорректное значение, попробуйте снова.");
                }
            }
            return size;
        }
        private static double[] GetVector()
        {
            Console.WriteLine("Вектор:");
            double[] vector = new double[GetVectorSize()];
            for (int i = 0; i < vector.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Console.Write((i + 1) + ": ");
                        vector[i] = Convert.ToDouble(GetInput());
                        flag = false;
                    }
                    catch (FormatException)
                    {
                        ShowException("Некорректное значение, попробуйте снова.");
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
                        vector[i] = Convert.ToDouble(GetInput());
                        flag = false;
                    }
                    catch (FormatException)
                    {
                        ShowException("Некорректное значение, попробуйте снова.");
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
        #endregion

        #region Matrix input/output.
        private static int GetNumberOfRows()
        {
            int rows = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Кол-во строк: ");
                    rows = Int16.Parse(GetInput());
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException("Некорректное значение, попробуйте снова.");
                }
            }
            return rows;
        }
        private static int GetNumberOfCols()
        {
            int cols = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Кол-во столбцов: ");
                    cols = Int16.Parse(GetInput());
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException("Некорректное значение, попробуйте снова.");
                }
            }
            return cols;
        }
        private static double[,] GetMatrix()
        {
            Console.WriteLine("Матрица:");
            double[,] matrix = new double[GetNumberOfRows(), GetNumberOfCols()];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Console.Write((i + 1) + ": ");
                        string s = GetInput();
                        int j = 0;
                        foreach (int v in s.Split(' ').Select(v => Convert.ToInt32(v)))
                        {
                            matrix[i, j++] = v;
                        }
                        flag = false;
                    }
                    catch (Exception)
                    {
                        ShowException("Некорректное значение, попробуйте снова.");
                    }
                }

            }
            return matrix;
        }
        private static double[,] GetMatrix(int index)
        {
            Console.WriteLine("Матрица " + index + ":");
            double[,] matrix = new double[GetNumberOfRows(), GetNumberOfCols()];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Console.Write((i + 1) + ": ");
                        string s = GetInput();
                        int j = 0;
                        foreach (int v in s.Split(' ').Select(v => Convert.ToInt32(v)))
                        {
                            matrix[i, j++] = v;
                        }
                        flag = false;
                    }
                    catch (Exception)
                    {
                        ShowException("Некорректное значение, попробуйте снова.");
                    }
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
        #endregion
    }
}
