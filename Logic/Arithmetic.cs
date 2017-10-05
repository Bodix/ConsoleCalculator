using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalculator
{
    class Arithmetic
    {
        public static double Calculate(string expression)
        {
            string[] array = expression.Split(' ');
            double result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == "+")
                    result = Add(Convert.ToDouble(array[i - 1]), Convert.ToDouble(array[i + 1]));
                if (array[i] == "-")
                    result = Deduct(Convert.ToDouble(array[i - 1]), Convert.ToDouble(array[i + 1]));
                if (array[i] == "*")
                    result = Multiply(Convert.ToDouble(array[i - 1]), Convert.ToDouble(array[i + 1]));
                if (array[i] == "/")
                    result = Divide(Convert.ToDouble(array[i - 1]), Convert.ToDouble(array[i + 1]));
            }
            return result;
        }
        private static double Add(double a, double b) { return a + b; }
        private static double Deduct(double a, double b) { return a - b; }
        private static double Multiply(double a, double b) { return a * b; }
        private static double Divide(double a, double b) { return a / b; }
    }
}
