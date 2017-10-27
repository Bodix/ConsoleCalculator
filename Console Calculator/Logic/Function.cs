using System;

namespace ConsoleCalculator
{
    class Function
    {
        public struct Point
        {
            public Point(double x, double y) : this()
            {
                X = x;
                Y = y;
            }
            public double X { get; private set; }
            public double Y { get; private set; }
        }
        public static double F(double x)
        {
            //return x * x - 25;
            return Math.Pow(x, 3) - 18 * x - 83;
        }
        public static double DF(double x)
        {
            double e = 10E-10;
            return (F(x + e) - F(x - e)) / (2 * e);
        }
        public static double D2F(double x)
        {
            double e = 10E-10;
            return (DF(x + e) - DF(x - e)) / (2 * e);
        }
    }
}
