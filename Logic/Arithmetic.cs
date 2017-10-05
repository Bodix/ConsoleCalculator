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
            List<string> list = expression.Split(' ').ToList();
            while (list.Count != 1)
            {
                MultiplyAndDivide(list);
                AddAndDeduct(list);
            }
            return Convert.ToDouble(list[0]);
        }
        
        private static void MultiplyAndDivide(List<string> list)
        {
            int index = 0;
            do
            {
                index = list.IndexOf("*");
                if (index != -1)
                {
                    list[index] = Multiply(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
            while (index != -1);
            do
            {
                index = list.IndexOf("/");
                if (index != -1)
                {
                    list[index] = Divide(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
            while (index != -1);
        }
        private static void AddAndDeduct(List<string> list)
        {
            int index = 0;
            do
            {
                index = list.IndexOf("+");
                if (index != -1)
                {
                    list[index] = Add(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
            while (index != -1);
            do
            {
                index = list.IndexOf("-");
                if (index != -1)
                {
                    list[index] = Deduct(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
            while (index != -1);
        }
        private static double Add(double a, double b) { return a + b; }
        private static double Deduct(double a, double b) { return a - b; }
        private static double Multiply(double a, double b) { return a * b; }
        private static double Divide(double a, double b) { return a / b; }
    }
}
