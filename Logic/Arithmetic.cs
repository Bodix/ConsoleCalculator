using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    class Arithmetic
    {
        public static double Solve(string expression)
        {
            if (expression.IndexOf("(") == -1)
            {
                return Calculate(expression);
            }
            return Solve(OpenBrackets(expression));
        }
        private static string OpenBrackets(string expression)
        {
            int indexBegin = expression.IndexOf("(");
            int indexEnd = expression.LastIndexOf(")");
            int length = indexEnd - indexBegin;
            string newExpression = expression.Substring(indexBegin + 1, length - 1);
            if (newExpression.IndexOf("(") == -1)
            {
                return expression.Replace("(" + newExpression + ")", Calculate(newExpression).ToString());
            }
            return expression.Replace("(" + newExpression + ")", Calculate(OpenBrackets(newExpression)).ToString());
        }

        private static double Calculate(string expression)
        {
            List<string> list = Regex.Split(expression, @"(\+)|(-)|(\*)|(/)").ToList();
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
            while (index != -1)
            {
                index = list.IndexOf("*");
                if (index != -1)
                {
                    list[index] = Multiply(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
            index = 0;
            while (index != -1)
            {
                index = list.IndexOf("/");
                if (index != -1)
                {
                    list[index] = Divide(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
        }
        private static void AddAndDeduct(List<string> list)
        {
            int index = 0;
            while (index != -1)
            {
                index = list.IndexOf("+");
                if (index != -1)
                {
                    list[index] = Add(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
            index = 0;
            while (index != -1)
            {
                index = list.IndexOf("-");
                if (index != -1)
                {
                    list[index] = Deduct(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
        }
        private static double Add(double a, double b) { return a + b; }
        private static double Deduct(double a, double b) { return a - b; }
        private static double Multiply(double a, double b) { return a * b; }
        private static double Divide(double a, double b) { return a / b; }
    }
}
