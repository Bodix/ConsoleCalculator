using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    class View
    {
        static string cancelCommand = "/c";
        static string formatException = "Ошибка: Некорректное значение, попробуйте снова. Используйте /c для отмены.";

        public static void GetCommand()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            WriteLine("\nВведите команду:");
            Write("> ");
            string command = GetInput().Trim(' ').ToLower();
            if (command == "/calc")
            {
                try
                {
                    ShowNumber(Arithmetic.Solve(GetExpression()));
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    GetCommand();
                }
            }
            if (command == "/vec")
            {
                WriteLine(
@"Доступные действия:
v+v - сложение векторов
v-v - вычитание векторов
v*n - умножение вектора на число
v*v - скалярное произведение векторов
len - длина вектора");
                Write("Действие: ");
                int vectorSize;
                double[] v1, v2;
                try
                {
                    switch (GetInput().Trim(' ').ToLower())
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
                        case "v*v":
                            vectorSize = GetVectorSize();
                            v1 = GetVector(vectorSize, 1);
                            v2 = GetVector(vectorSize, 2);
                            ShowNumber(Vector.MultiplyScalar(v1, v2));
                            break;
                        case "len":
                            v1 = GetVector();
                            ShowNumber(Vector.GetVectorLength(v1));
                            break;
                    }
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    GetCommand();
                }
            }
            if (command == "/mat")
            {
                WriteLine(
@"Доступные действия:
m+m - сложение матриц
m-m - вычитание матриц
m*n - умножение матрицы на число
m*v - умножение матрицы на вектор
m*m - умножение матрицы на матрицу
trans - транспонирование матрицы
det - определитель матрицы
inv - обратная матрица");
                Write("Действие: ");
                double[,] m1, m2;
                try
                {
                    switch (GetInput().Trim(' ').ToLower())
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
                        case "trans":
                            m1 = GetMatrix();
                            ShowMatrix(Matrix.GetTransposeMatrix(m1));
                            break;
                        case "det":
                            m1 = GetMatrix();
                            ShowNumber(Matrix.GetDeterminant(m1));
                            break;
                        case "inv":
                            m1 = GetMatrix();
                            ShowMatrix(Matrix.GetInvertMatrix(m1));
                            break;
                    }
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    GetCommand();
                }
            }
            if (command == "/lin")
            {
                WriteLine(
@"Доступные действия:
inv - решить СЛАУ методом обратной матрицы
iter - решить СЛАУ методом простых итераций
gauss - решить СЛАУ методом Гаусса");
                Write("Действие: ");
                double[,] a;
                double[] b;
                try
                {
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "inv":
                            a = GetMatrix();
                            b = GetVector();
                            ShowVector(LinearSystem.SolveLSByInvertMatrix(a, b));
                            break;
                        case "iter":
                            a = GetMatrix();
                            b = GetVector();
                            ShowVector(LinearSystem.SolveLSByIteration(LinearSystem.ConvertToExtendedMatrix(a, b)));
                            break;
                        case "gauss":
                            a = GetMatrix();
                            b = GetVector();
                            ShowVector(LinearSystem.SolveLSByGauss(LinearSystem.ConvertToExtendedMatrix(a, b)));
                            break;
                    }
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    GetCommand();
                }
            }
            if (command == "/nlin")
            {
                WriteLine(
@"Доступные действия:
bis - решить нелинейное уравнение методом половинного деления (бисекции)
ch - решить нелинейное уравнение методом хорд");
                Write("Действие: ");
                double a;
                double b;
                try
                {
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "bis":
                            a = GetNumber();
                            b = GetNumber();
                            ShowNumber(AlgebraicEquations.SolveByBisection(a, b));
                            break;
                        case "ch":
                            a = GetNumber();
                            b = GetNumber();
                            ShowNumber(AlgebraicEquations.SolveByChords(a, b));
                            break;
                    }
                }
                catch (Exception e)
                {
                    ShowException(e.Message);
                    GetCommand();
                }
            }
            if (command == "/help")
            {
                WriteLine("Список команд:\n/calc - арифметические операции\n/vec - операции с векторами\n/mat - операции с матрицами\n/lin - операции с системами линейных алгебраических уравнений (СЛАУ)\n/nlin - операции с нелинейными уравнений\n/clr - очистить консоль\n/q - выход");
                GetCommand();
            }
            if (command == "/clr")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
                GetCommand();
            }
            if (command == "/q")
            {
                Environment.Exit(0);
            }
            else
            {
                WriteLine("Команда не найдена (/help).");
                GetCommand();
            }
        }
        private static string GetInput()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine();
            Log.WriteLine(input);
            Console.ForegroundColor = ConsoleColor.Yellow;
            return input;
        }
        private static string GetExpression()
        {
            Write("Введите выражение: ");
            string expression = GetInput();
            if (Regex.IsMatch(expression, @"[^0-9 \+\-\*\^/\(\)pePE]")) throw new Exception("Ошибка: Выражение содержит недопустимый символ.");
            if (expression.Contains('(') | expression.Contains(')'))
            {
                int i = 0;
                int balance = 0;
                while (i < expression.Length)
                {
                    if (expression[i] == '(') balance++;
                    if (expression[i] == ')') balance--; if (balance < 0) break;
                    i++;
                }
                if (balance != 0)
                    throw new Exception("Ошибка: В выражении не закрыты скобки.");
            }
            expression = Regex.Replace(expression, @"p", Math.PI.ToString());
            expression = Regex.Replace(expression, @"e", Math.E.ToString());
            return expression;
        }
        private static void Write(string text)
        {
            Console.Write(text);
            Log.Write(text);
        }
        private static void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
            Log.Write(format, arg);
        }
        private static void WriteLine(string text)
        {
            Console.WriteLine(text);
            Log.WriteLine(text);
        }
        private static void WriteLine()
        {
            Console.WriteLine();
            Log.WriteLine();
        }
        private static void ShowException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(message);
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
                    Write("Число: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        GetCommand();
                    }
                    number = Convert.ToDouble(input);
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
            WriteLine("Результат: " + number);
            GetCommand();
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
                    Write("Размер: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        GetCommand();
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
            WriteLine("Вектор:");
            double[] vector = new double[GetVectorSize()];
            for (int i = 0; i < vector.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Write((i + 1) + ": ");
                        string input = GetInput();
                        if (input == cancelCommand)
                        {
                            GetCommand();
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
            WriteLine("Вектор " + index + ":");
            for (int i = 0; i < vector.Length; i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Write((i + 1) + ": ");
                        string input = GetInput();
                        if (input == cancelCommand)
                        {
                            GetCommand();
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
            WriteLine("Результат: ");
            for (int i = 0; i < vector.Length; i++)
                Write("{0:0.###}\t", vector[i]);
            WriteLine();
            GetCommand();
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
                    Write("Кол-во строк: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        GetCommand();
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
                    Write("Кол-во столбцов: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        GetCommand();
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
            WriteLine("Матрица:");
            double[,] matrix = new double[GetNumberOfRows(), GetNumberOfCols()];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Write((i + 1) + ": ");
                        string input = GetInput();
                        if (input == cancelCommand)
                        {
                            GetCommand();
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
            WriteLine("Матрица " + index + ":");
            double[,] matrix = new double[GetNumberOfRows(), GetNumberOfCols()];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool flag = true;
                while (flag)
                {
                    try
                    {
                        Write((i + 1) + ": ");
                        string input = GetInput();
                        if (input == cancelCommand)
                        {
                            GetCommand();
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
            WriteLine("Результат: ");
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    Write("{0:0.###}\t", matrix[i, j]);
                }
                WriteLine();
            }
            GetCommand();
        }
        #endregion
    }
}
