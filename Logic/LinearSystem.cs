using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalculator.Logic
{
    class LinearSystem
    {
        public double[] SolveLSByInvertMatrix(double[,] a, double[] b)
        {
            return Matrix.MultiplyMatrixToVector(Matrix.GetInvertMatrix(a), b);
        }
    }
}
