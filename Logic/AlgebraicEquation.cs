using System;

namespace ConsoleCalculator
{
    class AlgebraicEquations
    {
        public static double eps = 1e-5;

        public static double SolveByBisection(double a, double b)
        {
            if (Function.F(a) * Function.F(b) >= 0)
            {
                throw new Exception("Ошибка: F(a) и F(b) должны иметь разные знаки.");
            }
            double m = 0;
            while (Math.Abs(b - a) > eps && Function.F(m) != 0)
            {
                m = (a + b) / 2;
                if (Function.F(a) * Function.F(m) > 0) b = m;
                if (Function.F(b) * Function.F(m) > 0) a = m;
            }
            return m;
        }
        public static double SolveByChords(double a, double b)
        {
            double m = 0;
            double tmp;
            do
            {
                tmp = m;
                m = b - Function.F(b) * (a - b) / (Function.F(a) - Function.F(b));
                a = b;
                b = tmp;
            }
            while (Math.Abs(m - b) > eps);
            return m;
        }
    }
}
