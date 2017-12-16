using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    class View
    {
        private static string cancelCommand = "/c";
        private static string formatMessage = "Некорректное значение, попробуйте снова\nИспользуйте /c для отмены";
        private static CancelException cancelException = new CancelException("Отмена операции");

        public static void Calculate()
        {
            try
            {
                string command = GetCommand();
                if (command == "/calc")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "---===Арифметический калькулятор===---",
                        "Доступные операции: + - * / ^",
                        "Доступно использование круглых скобок и констант:",
                        "p = 3,14159 (число Пи)",
                        "e = 2,71828 (число Эйлера)"
                        ));
                    PrintNumber(Arithmetic.Solve(GetExpression()));
                }
                else if (command == "/vec")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "---===Векторы===---",
                        "Доступные действия:",
                        "v+v - сложение векторов",
                        "v-v - вычитание векторов",
                        "v*n - умножение вектора на число",
                        "v*v - скалярное произведение векторов",
                        "len - длина вектора"));
                    Write("Действие: ");
                    int vectorSize;
                    double[] v1, v2;
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "v+v":
                            vectorSize = GetVectorSize();
                            v1 = GetVector("1", vectorSize);
                            v2 = GetVector("2", vectorSize);
                            PrintVector(Vector.AddVectorToVector(v1, v2));
                            break;
                        case "v-v":
                            vectorSize = GetVectorSize();
                            v1 = GetVector("1", vectorSize);
                            v2 = GetVector("2", vectorSize);
                            PrintVector(Vector.DeductVectorFromVector(v1, v2));
                            break;
                        case "v*n":
                            v1 = GetVector();
                            double n = GetNumber();
                            PrintVector(Vector.MultiplyVectorToNumber(v1, n));
                            break;
                        case "v*v":
                            vectorSize = GetVectorSize();
                            v1 = GetVector("1", vectorSize);
                            v2 = GetVector("2", vectorSize);
                            PrintNumber(Vector.MultiplyScalar(v1, v2));
                            break;
                        case "len":
                            v1 = GetVector();
                            PrintNumber(Vector.GetVectorLength(v1));
                            break;
                    }
                }
                else if (command == "/mat")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "---===Матрицы===---",
                        "Доступные действия:",
                        "m+m - сложение матриц",
                        "m-m - вычитание матриц",
                        "m*n - умножение матрицы на число",
                        "m*v - умножение матрицы на вектор",
                        "m*m - умножение матрицы на матрицу",
                        "trans - транспонирование матрицы",
                        "det - определитель матрицы",
                        "inv - обратная матрица"));
                    Write("Действие: ");
                    double[,] m1, m2;
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "m+m":
                            m1 = GetMatrix("1");
                            m2 = GetMatrix("2");
                            PrintMatrix(Matrix.AddMatrixToMatrix(m1, m2));
                            break;
                        case "m-m":
                            m1 = GetMatrix("1");
                            m2 = GetMatrix("2");
                            PrintMatrix(Matrix.DeductMatrixFromMatrix(m1, m2));
                            break;
                        case "m*n":
                            m1 = GetMatrix();
                            double n = GetNumber();
                            PrintMatrix(Matrix.MultiplyMatrixToNumber(m1, n));
                            break;
                        case "m*v":
                            m1 = GetMatrix();
                            double[] v = GetVector();
                            PrintVector(Matrix.MultiplyMatrixToVector(m1, v));
                            break;
                        case "m*m":
                            m1 = GetMatrix("1");
                            m2 = GetMatrix("2");
                            PrintMatrix(Matrix.MultiplyMatrixToMatrix(m1, m2));
                            break;
                        case "trans":
                            m1 = GetMatrix();
                            PrintMatrix(Matrix.GetTransposeMatrix(m1));
                            break;
                        case "det":
                            m1 = GetMatrix();
                            PrintNumber(Matrix.GetDeterminant(m1));
                            break;
                        case "inv":
                            m1 = GetMatrix();
                            PrintMatrix(Matrix.GetInvertMatrix(m1));
                            break;
                    }
                }
                else if (command == "/lin")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "---===СЛАУ===---",
                        "Доступные действия:",
                        "inv - решить СЛАУ методом обратной матрицы",
                        "iter - решить СЛАУ методом простой итерации",
                        "gauss - решить СЛАУ методом Гаусса"));
                    Write("Действие: ");
                    double[,] a;
                    double[] b;
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "inv":
                            a = GetMatrix("коэффициентов");
                            b = GetVector("свободных членов", a.GetLength(0));
                            PrintVector(LinearSystem.SolveByInvertMatrix(a, b));
                            break;
                        case "iter":
                            a = GetMatrix("коэффициентов");
                            b = GetVector("свободных членов", a.GetLength(0));
                            PrintVector(LinearSystem.SolveByIteration(LinearSystem.ConvertToExtendedMatrix(a, b)));
                            break;
                        case "gauss":
                            a = GetMatrix("коэффициентов");
                            b = GetVector("свободных членов", a.GetLength(0));
                            PrintVector(LinearSystem.SolveByGauss(LinearSystem.ConvertToExtendedMatrix(a, b)));
                            break;
                    }
                }
                else if (command == "/nlin")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Внимание!");
                    Console.WriteLine("Отсутствует автоматическая проверка на сходимость методов");
                    Console.WriteLine("Возможно образование бесконечного цикла");
                    Console.ForegroundColor = ConsoleColor.White;
                    WriteLine(String.Join(Environment.NewLine,
                        "---===Нелинейные уравнения===---",
                        "Доступные действия:",
                        "q - решить квадратное уравнение",
                        "bis - решить нелинейное уравнение методом половинного деления",
                        "ch - решить нелинейное уравнение методом хорд",
                        "new - решить нелинейное уравнение методом Ньютона"));
                    Write("Действие: ");
                    double a;
                    double b;
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "q":
                            WriteLine("Коэффициенты квадратного уравнения ax^2+bx+c=0:");
                            a = GetNumber("a");
                            b = GetNumber("b");
                            double c = GetNumber("c");
                            WriteLine(AlgebraicEquations.GetRoots(a, b, c));
                            break;
                        case "bis":
                            WriteLine("Интервал [a, b]:");
                            a = GetNumber("a");
                            b = GetNumber("b");
                            PrintNumber(AlgebraicEquations.SolveByBisection(a, b));
                            break;
                        case "ch":
                            WriteLine("Интервал [a, b]:");
                            a = GetNumber("a");
                            b = GetNumber("b");
                            PrintNumber(AlgebraicEquations.SolveByChords(a, b));
                            break;
                        case "new":
                            WriteLine("Интервал [a, b]:");
                            a = GetNumber("a");
                            b = GetNumber("b");
                            PrintNumber(AlgebraicEquations.SolveByNewtone(a, b));
                            break;
                    }
                }
                else if (command == "/int")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "---===Интерполяция===---",
                        "Доступные действия:",
                        "near - интерполяция методом ближайшего соседа",
                        "lin - линейная интерполяция",
                        "poly - полиномиальная интерполяция"));
                    Write("Действие: ");
                    Point[] p;
                    double x;
                    switch (GetInput().Trim(' ').ToLower())
                    {
                        case "near":
                            p = GetPoints();
                            x = GetNumber("X");
                            PrintNumber(Interpolation.NearestNeighbor(p, x));
                            break;
                        case "lin":
                            p = GetPoints();
                            x = GetNumber("X");
                            PrintNumber(Interpolation.Linear(p, x));
                            break;
                        case "poly":
                            p = GetPoints();
                            x = GetNumber("X");
                            PrintNumber(Interpolation.Polynomial(p, x));
                            break;
                    }
                }
                else if (command == "/ex")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "---===Примеры ввода===---",
                        "Задача:",
                        "Найти корень из 2+3*2 с помощью арифметического калькулятора",
                        "Ввод: (2+3*2)^(1/2)",
                        string.Empty,
                        "Задача:",
                        "Возвести экспоненту (число Эйлера) во вторую степень",
                        "с помощью арифметического калькулятора",
                        "Ввод: e^2",
                        string.Empty,
                        "Задача:",
                        "Ввести произвольную матрицу 2х3",
                        "Ввод:",
                        "1: -23,2 -31 351",
                        "2: 244,04 0,2 -1,5",
                        string.Empty,
                        "Задача:",
                        "Ввести точки с координатами (4; -1,5) (2; 5) (0,7; -3)",
                        "Ввод:",
                        "1: 4 -1,5",
                        "2: 2 5",
                        "3: 0,7 -3"));
                }
                else if (command == "/nan")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "NaN (англ. Not-a-Number, «нечисло») — одно из особых состояний числа с",
                        "плавающей запятой. Данное состояние может возникнуть в различных случаях,",
                        "например, когда предыдущая математическая операция завершилась с неопределённым",
                        "результатом или если в ячейку памяти попало не удовлетворяющее условиям число",
                        string.Empty,
                        "Операции, приводящие к появлению NaN в качестве ответа:",
                        "1. Все математические операции, содержащие NaN в качестве одного из операндов",
                        "2. Деление нуля на ноль",
                        "3. Деление бесконечности на бесконечность",
                        "4. Умножение нуля на бесконечность",
                        "5. Сложение бесконечности с бесконечностью противоположного знака",
                        "6. Вычисление квадратного корня отрицательного числа",
                        "7. Логарифмирование отрицательного числа",
                        string.Empty,
                        "Метод возведения в степень Math.Pow(x, y) или x^y возвращает NaN, если:",
                        "x < 0, но не -бесконечность",
                        "y = не целое число, -бесконечность или бесконечность"));
                }
                else if (command == "/help")
                {
                    WriteLine(String.Join(Environment.NewLine,
                        "Список команд:",
                        "/calc - арифметические операции",
                        "/vec - операции с векторами",
                        "/mat - операции с матрицами",
                        "/lin - операции с системами линейных алгебраических уравнений",
                        "/nlin - операции с нелинейными уравнениями",
                        "/int - интерполяция",
                        "/ex - примеры ввода",
                        "/nan - что такое NaN?",
                        "/clr - очистить консоль",
                        "/q - выход"
                        ));
                }
                else if (command == "/clr")
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
                }
                else if (command == "/q")
                {
                    Environment.Exit(0);
                }
                else
                    WriteLine("Команда не найдена (/help)");
            }
            catch (Exception e)
            {
                PrintException(e.Message);
            }
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
        private static string GetInput()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            string input = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.White;
            Log.WriteLine(input);
            return input;
        }
        private static string GetCommand()
        {
            WriteLine("\nВведите команду:");
            Write("> ");
            return GetInput().Trim(' ').ToLower();

        }
        private static string GetExpression()
        {
            Write("Введите выражение: ");
            string expression = GetInput().ToLower();
            if (Regex.IsMatch(expression, @"[^0-9 \+\-\*\^/\(\)pe]"))
                throw new Exception("Выражение содержит недопустимый символ");
            if (expression.Contains('(') | expression.Contains(')'))
            {
                int balance = 0;
                for (int i = 0; i < expression.Length; i++)
                {
                    if (expression[i] == '(') balance++;
                    if (expression[i] == ')') balance--;
                    if (balance < 0) break;
                }
                if (balance != 0)
                    throw new Exception("В выражении не закрыты скобки");
            }
            expression = Regex.Replace(expression, @"p", Math.PI.ToString());
            expression = Regex.Replace(expression, @"e", Math.E.ToString());
            return expression;
        }
        private static void PrintException(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static double GetNumber()
        {
            double number = 0;
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Write("Число: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    number = Convert.ToDouble(input);
                    flag = false;
                }
                catch (FormatException)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return number;
        }
        private static double GetNumber(string description)
        {
            double number = 0;
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Write(description + ": ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    number = Convert.ToDouble(input);
                    flag = false;
                }
                catch (FormatException)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return number;
        }
        private static void PrintNumber(double number)
        {
            WriteLine("Результат: " + number);
        }

        private static int GetVectorSize()
        {
            int size = 0;
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Write("Размер: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    size = Int16.Parse(input);
                    if (size <= 0)
                        throw new ArgumentException("Размер вектора должен быть положительным");
                    flag = false;
                }
                catch (ArgumentException e)
                {
                    PrintException(e.Message);
                }
                catch (FormatException)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return size;
        }
        private static double[] GetVector()
        {
            double[] vector = new double[GetVectorSize()];
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    WriteLine("Вектор: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    int i = 0;
                    foreach (string v in input.Split(' '))
                    {
                        vector[i++] = Convert.ToDouble(v);
                    }
                    flag = false;
                }
                catch (Exception)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return vector;
        }
        private static double[] GetVector(string description, int size)
        {
            double[] vector = new double[size];
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    WriteLine("Вектор " + description + ": ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    int i = 0;
                    foreach (string v in input.Split(' '))
                    {
                        vector[i++] = Convert.ToDouble(v);
                    }
                    flag = false;
                }
                catch (Exception)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return vector;
        }
        private static void PrintVector(double[] vector)
        {
            WriteLine("Результат: ");
            for (int i = 0; i < vector.Length; i++)
                Write("{0:0.#####}\t", vector[i]);
            WriteLine();
        }

        private static int GetNumberOfRows()
        {
            int rows = 0;
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Write("Кол-во строк: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    rows = Int16.Parse(input);
                    if (rows <= 0)
                        throw new ArgumentException("Количество строк должно быть положительным");
                    flag = false;
                }
                catch (ArgumentException e)
                {
                    PrintException(e.Message);
                }
                catch (FormatException)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return rows;
        }
        private static int GetNumberOfCols()
        {
            int cols = 0;
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Write("Кол-во столбцов: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    cols = Int16.Parse(input);
                    if (cols <= 0)
                        throw new ArgumentException("Количество столбцов должно быть положительным");
                    flag = false;
                }
                catch (ArgumentException e)
                {
                    PrintException(e.Message);
                }
                catch (FormatException)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return cols;
        }
        private static double[,] GetMatrix()
        {
            WriteLine("Матрица:");
            double[,] matrix = new double[GetNumberOfRows(), GetNumberOfCols()];
            bool cancel = false;
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
                            cancel = true;
                            break;
                        }
                        int j = 0;
                        foreach (string v in input.Split(' '))
                        {
                            matrix[i, j++] = Convert.ToDouble(v);
                        }
                        flag = false;
                    }
                    catch (Exception)
                    {
                        PrintException(formatMessage);
                    }
                }
            }
            if (cancel == true)
                throw cancelException;
            return matrix;
        }
        private static double[,] GetMatrix(string description)
        {
            double[,] matrix = new double[GetNumberOfRows(), GetNumberOfCols()];
            bool cancel = false;
            WriteLine("Матрица " + description + ":");
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
                            cancel = true;
                            break;
                        }
                        int j = 0;
                        foreach (string v in input.Split(' '))
                        {
                            matrix[i, j++] = Convert.ToDouble(v);
                        }
                        flag = false;
                    }
                    catch (Exception)
                    {
                        PrintException(formatMessage);
                    }
                }
            }
            if (cancel == true)
                throw cancelException;
            return matrix;
        }
        private static void PrintMatrix(double[,] matrix)
        {
            WriteLine("Результат: ");
            for (int i = 0; i < matrix.GetLength(0); ++i)
            {
                for (int j = 0; j < matrix.GetLength(1); ++j)
                {
                    Write("{0:0.#####}\t", matrix[i, j]);
                }
                WriteLine();
            }
        }

        private static int GetNumberOfPoints()
        {
            int number = 0;
            bool cancel = false;
            bool flag = true;
            while (flag)
            {
                try
                {
                    Write("Кол-во точек: ");
                    string input = GetInput();
                    if (input == cancelCommand)
                    {
                        cancel = true;
                        break;
                    }
                    number = Int16.Parse(input);
                    if (number <= 0)
                        throw new ArgumentException("Количество точек должно быть положительное");
                    flag = false;
                }
                catch (ArgumentException e)
                {
                    PrintException(e.Message);
                }
                catch (FormatException)
                {
                    PrintException(formatMessage);
                }
            }
            if (cancel == true)
                throw cancelException;
            return number;
        }
        private static Point[] GetPoints()
        {
            Point[] points = new Point[GetNumberOfPoints()];
            bool cancel = false;
            WriteLine("Точки:");
            for (int i = 0; i < points.Length; i++)
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
                            cancel = true;
                            break;
                        }
                        if (input.Split(' ').Length > 2) throw new FormatException();
                        points[i] = new Point(Convert.ToDouble(input.Split(' ')[0]),
                            Convert.ToDouble(input.Split(' ')[1]));
                        flag = false;
                    }
                    catch (Exception)
                    {
                        PrintException(formatMessage);
                    }
                }
            }
            if (cancel == true)
                throw cancelException;
            points = points.OrderBy(X => X.X).ToArray();
            return points;
        }
    }

    class CancelException : Exception
    {
        public CancelException(string message) : base(message) { }
    }
}
