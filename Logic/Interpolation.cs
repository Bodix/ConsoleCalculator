using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalculator
{
    class Interpolation
    {
        public static double NearestNeighbor(Function.Point[] p, double x)
        {
            double y = 0;
            bool flag = false;
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (p[i].X <= x && x <= p[i + 1].X)
                {
                    double m = (p[i + 1].X - p[i].X) / 2;
                    if (x <= p[i].X + m)
                    {
                        y = p[i].Y;
                        flag = true;
                        break;
                    }
                    if (x >= p[i].X + m)
                    {
                        y = p[i + 1].Y;
                        flag = true;
                        break;
                    }
                }
            }
            if (flag == false)
            {
                if (x <= p[0].X)
                    y = p[0].Y;
                if (p[p.Length - 1].X <= x)
                    y = p[p.Length - 1].Y;
            }
            return y;
        }
        public static double Linear(Function.Point[] p, double x)
        {
            double y = 0;
            bool flag = false;
            for (int i = 0; i < p.Length - 1; i++)
            {
                if (p[i].X <= x && x <= p[i + 1].X)
                {
                    double k = (p[i + 1].Y - p[i].Y) / (p[i + 1].X - p[i].X);
                    double b = p[i].Y - k * p[i].X;
                    y = k * x + b;
                    flag = true;
                    break;
                }
            }
            if (flag == false)
                throw new Exception("Ошибка: Значение Х вне диапазона линейной интерполяции.");
            return y;
        }
        public static double Polynomial(Function.Point[] p, double x)
        {
            double y = 0;
            double[,] a = new double[p.Length, p.Length];
            for (int row = 0; row < a.GetLength(0); row++)
                for (int col = 0; col < a.GetLength(1); col++)
                {
                    a[row, col] = Math.Pow(p[row].X, col);
                }
            double[] b = new double[p.Length];
            for (int i = 0; i < b.Length; i++)
                b[i] = p[i].Y;
            double[] c = LinearSystem.SolveByInvertMatrix(a, b);
            for (int i = 0; i < c.Length; i++)
                y += c[i] * Math.Pow(x, i);
            return y;
        }
    }
}
