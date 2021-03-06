﻿using System;

namespace ConsoleCalculator
{
    class Vector
    {
        private static string sizeException = "Размер векторов не совпадает";

        public static double[] AddVectorToVector(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
                throw new Exception(sizeException);
            double[] result = new double[vector1.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = vector1[i] + vector2[i];
            return result;
        }
        public static double[] DeductVectorFromVector(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
                throw new Exception(sizeException);
            double[] result = new double[vector1.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = vector1[i] - vector2[i];
            return result;
        }
        public static double[] MultiplyVectorToNumber(double[] vector1, double vector2)
        {
            double[] result = new double[vector1.Length];
            for (int i = 0; i < result.Length; i++)
                result[i] = vector1[i] * vector2;
            return result;
        }
        public static double MultiplyScalar(double[] vector1, double[] vector2)
        {
            if (vector1.Length != vector2.Length)
                throw new Exception(sizeException);
            double sum = 0;
            for (int i = 0; i < vector1.Length; i++)
                sum += vector1[i] * vector2[i];
            return sum;
        }
        public static double GetVectorLength(double[] vector)
        {
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
                sum += vector[i] * vector[i];
            return Math.Sqrt(sum);
        }
    }
}
