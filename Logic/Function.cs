using System;

namespace ConsoleCalculator
{
    class Function
    {
        public struct Point
        {
            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
            public double X { get; private set; }
            public double Y { get; private set; }
        }
        public static double F(double x)
        {
            return Math.Pow(x, 3) - 18 * x - 83;
        }
    }
}
