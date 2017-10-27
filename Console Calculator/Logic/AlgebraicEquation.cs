using System;

namespace ConsoleCalculator
{
    class AlgebraicEquations
    {
        public static double eps = 1e-5;

        public static double SolveByBisection(double a, double b)
        {
            double x = 0;
            if (!IsIntervalSuitable(a, b, out x))
            {
                return x;
            }
            do
            {
                x = (a + b) / 2;
                if (Function.F(a) * Function.F(x) > 0) a = x;
                if (Function.F(b) * Function.F(x) > 0) b = x;
            }
            while (Math.Abs(a - b) >= eps);
            return x;
        }
        public static double SolveByChords(double a, double b)
        {
            double x = 0;
            if (!IsIntervalSuitable(a, b, out x))
            {
                return x;
            }
            do
            {
                x = b - Function.F(b) * (a - b) / (Function.F(a) - Function.F(b));
                if (Function.F(a) * Function.F(x) > 0) a = x;
                if (Function.F(x) * Function.F(b) > 0) b = x;
            }
            while (Math.Abs(Function.F(x)) >= eps);
            return x;
        }
        public static double SolveByNewtone(double a, double b)
        {
            double x = 0;
            if (!IsIntervalSuitable(a, b, out x))
            {
                return x;
            }
            double x0;
            if (Function.F(a) * Function.D2F(a) > 0) x0 = a;
            else x0 = b;
            x = x0;
            do
            {
                x0 = x;
                x = x0 - (Function.F(x0) / Function.DF(x0));
                while (x < a || b < x)
                    x = (x0 + x) / 2;
            }
            while (Math.Abs(x0 - x) >= eps);
            return x;
        }
        private static bool IsIntervalSuitable(double a, double b, out double x)
        {
            if (Function.F(a) == 0)
            {
                x = a;
                return false;
            }
            if (Function.F(b) == 0)
            {
                x = b;
                return false;
            }
            if (Function.F(0) == 0)
            {
                x = 0;
                return false;
            }
            if (Function.F(a) * Function.F(b) > 0)
                throw new Exception("Ошибка: F(a) и F(b) должны иметь разные знаки.");
            x = 0;
            return true;
        }
    }
}