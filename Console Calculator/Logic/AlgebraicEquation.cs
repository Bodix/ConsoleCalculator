using System;
using System.Numerics;
using System.Collections.Generic;

namespace ConsoleCalculator
{
    class AlgebraicEquations
    {
        public static double eps = 1e-5;

        public static string GetRoots(double a, double b, double c)
        {
            string result = string.Empty;
            if (a == 0)
            {
                if (b != 0)
                    return "Уравнение линейное, т.к. a = 0\nx = " + (-c / b);
                else if (c == 0)
                    return "Все коэффициенты равны нулю, x - любое число";
                else
                    return "Нет решений";
            }
            var roots = SolveQuadraticEquation(a, b, c);
            if (roots[0] == null)
            {
                var complexRoots = SolveQuadraticEquationComplex(a, b, c);
                result = BuildStringComplex(complexRoots);
            }
            else if (roots[1] == null)
            {
                result = "x1 = " + roots[0];
            }
            else
            {
                result = BuildString(roots);
            }
            return result;
        }
        private static List<double?> SolveQuadraticEquation(double a, double b, double c)
        {
            List<double?> result = new List<double?>(2);
            double? x1 = null;
            double? x2 = null;
            double det = b * b - 4 * a * c;
            if (det > 0)
            {
                x1 = (-b + Math.Sqrt(det)) / (2 * a);
                x2 = (-b - Math.Sqrt(det)) / (2 * a);
            }
            else if (det == 0)
            {
                x1 = (-b / (2 * a));
            }
            result.Add(x1);
            result.Add(x2);
            return result;
        }
        private static List<Complex> SolveQuadraticEquationComplex(double a, double b, double c)
        {
            List<Complex> result = new List<Complex>(2);
            double det = b * b - 4 * a * c;
            double absRoot = Math.Sqrt(Math.Abs(det));
            Complex root = det < 0 ? new Complex(0, absRoot) : new Complex(absRoot, 0);
            Complex q = -0.5 * (b + Math.Sign(b) * root);
            result.Add(q / a);
            result.Add(c / q);
            return result;
        }
        private static string BuildString(List<double?> roots)
        {
            string s = string.Empty;
            for (int i = 0; i < roots.Count; i++)
                s += String.Format("x{0} = {1:0.#####}\n", i + 1, roots[i]);
            return s;
        }
        private static string BuildStringComplex(List<Complex> complexRoots)
        {
            string s = string.Empty;
            for (int i = 0; i < complexRoots.Count; i++)
                s += String.Format("x{0} = {1:0.#####} + i*" + 
                    (complexRoots[i].Imaginary < 0 ? "({2:0.#####})" : "{2:0.#####}") + "\n",
                    i + 1, complexRoots[i].Real, complexRoots[i].Imaginary);
            return s;
        }

        public static double SolveByBisection(double a, double b)
        {
            if (!IsIntervalSuitable(a, b, out double x))
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
            if (!IsIntervalSuitable(a, b, out double x))
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
            if (!IsIntervalSuitable(a, b, out double x))
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
                throw new Exception("F(a) и F(b) должны иметь разные знаки");
            x = 0;
            return true;
        }
    }
}