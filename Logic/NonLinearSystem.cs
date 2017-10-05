using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace ConsoleCalculator
{
    class NonLinearSystem
    {
        public static double eps = 1e-5;

        public static double SolveByDichotomy(double a, double b)
        {
            while (Math.Abs(a - b) > eps)
            {
                double m = (a + b) / 2;
                if (Function.F(a) * Function.F(m) > 0)
                { a = m; }
                if (Function.F(b) * Function.F(m) > 0)
                { b = m; }
            }
            return (a + b) / 2;
        }
    }
}
