using System;
using System.Linq;

namespace ConsoleCalculator
{
    class View
    {
        static string formatException = "Ошибка: Некорректное значение, попробуйте снова. Используйте /cancel для отмены";

        public static void Calculate()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nВведите команду:");
            Console.Write("> ");
            string command = GetInput().ToLower();
            if (command == "/simple")
            {
                Console.WriteLine("Введите выражение: ");
                try
                {
                    ShowNumber(Arithmetic.Calculate(GetInput()));
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    Calculate();
                }
            }
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
                switch (GetInput().ToLower())
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
                        double n = GetNumber();
                        ShowVector(Vector.MultiplyVectorToNumber(v1, n));
                        break;
                    case "vLen":
                        v1 = GetVector();
                        ShowNumber(Vector.GetVectorLength(v1));
                        break;
                    case "v*v":
                        vectorSize = GetVectorSize();
                        v1 = GetVector(vectorSize, 1);
                        v2 = GetVector(vectorSize, 2);
                        ShowNumber(Vector.MultiplyScalar(v1, v2));
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
                    switch (GetInput().ToLower())
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
                            double n = GetNumber();
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
                            ShowNumber(Matrix.GetDeterminant(m1));
                            break;
                        case "mInv":
                            m1 = GetMatrix();
                            ShowMatrix(Matrix.GetInvertMatrix(m1));
                            break;
                    }
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    Calculate();
                }
            }
            if (command == "/linear")
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
                    switch (GetInput().ToLower())
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
                catch (Exception e)
                {
                    ShowException(e.Message);
                    Calculate();
                }
            }
            if (command == "/help")
            {
                Console.WriteLine("Список команд:\n/simple - простые операции\n/vector - операции с векторами\n/matrix - операции с матрицами\n/linear - операции с системами линейных алгебраических уравненй (СЛАУ)\n/clear - очистить консоль\n/exit - выход");
                Calculate();
            }
            if (command == "/clear")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
                Calculate();
            }
            if (command == "/exit")
            {
                Environment.Exit(0);
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

        #region Number input/output.
        private static double GetNumber()
        {
            double number = 0;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Console.Write("Число: ");
                    number = Convert.ToDouble(GetInput());
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException(formatException);
                }
            }
            return number;
        }
        private static void ShowNumber(double number)
        {
            Console.WriteLine("Результат: " + number);
            Calculate();
        }
        #endregion

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
                    string input = GetInput();
                    if (input == "/cancel")
                    {
                        Calculate();
                    }
                    size = Int16.Parse(input);
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException(formatException);
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
                        string input = GetInput();
                        if (input == "/cancel")
                        {
                            Calculate();
                        }
                        vector[i] = Convert.ToDouble(input);
                        flag = false;
                    }
                    catch (FormatException)
                    {
                        ShowException(formatException);
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
                        string input = GetInput();
                        if (input == "/cancel")
                        {
                            Calculate();
                        }
                        vector[i] = Convert.ToDouble(input);
                        flag = false;
                    }
                    catch (FormatException)
                    {
                        ShowException(formatException);
                    }
                }
            }
            return vector;
        }
        private static void ShowVector(double[] vector)
        {
            Console.WriteLine("Результат: ");
            for (int i = 0; i < vector.Length; i++)
                Console.Write("{0:0.###}\t", vector[i]);
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
                    string input = GetInput();
                    if (input == "/cancel")
                    {
                        Calculate();
                    }
                    rows = Int16.Parse(input);
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException(formatException);
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
                    string input = GetInput();
                    if (input == "/cancel")
                    {
                        Calculate();
                    }
                    cols = Int16.Parse(input);
                    flag = false;
                }
                catch (FormatException)
                {
                    ShowException(formatException);
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
                        string input = GetInput();
                        if (input == "/cancel")
                        {
                            Calculate();
                        }
                        int j = 0;
                        foreach (int v in input.Split(' ').Select(v => Convert.ToDouble(v)))
                        {
                            matrix[i, j++] = v;
                        }
                        flag = false;
                    }
                    catch (Exception)
                    {
                        ShowException(formatException);
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
                        string input = GetInput();
                        if (input == "/cancel")
                        {
                            Calculate();
                        }
                        int j = 0;
                        foreach (int v in input.Split(' ').Select(v => Convert.ToDouble(v)))
                        {
                            matrix[i, j++] = v;
                        }
                        flag = false;
                    }
                    catch (Exception)
                    {
                        ShowException(formatException);
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
                    Console.Write("{0:0.###}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Calculate();
        }
        #endregion
    }
}
