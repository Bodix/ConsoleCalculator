using System;

namespace ConsoleCalculator
{
    class Function
    {
        public static double F(double x)
        {
            return 2 * x - Math.Cos(x); // +++
            //return Math.Pow(x, 3) - 18 * x - 83; // +++
            //return 4 - Math.Exp(x) - 2 * x * 2; // +-+ ch (inf loop)
            //return x - Math.Pow(x, Math.E) + 3; // -++ bis (inf loop)
            //return x * x - 25; // ++- new (-6, 1)=1
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
}
