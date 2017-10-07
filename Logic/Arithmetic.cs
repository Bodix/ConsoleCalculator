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
            string newExpression = GetSubstring(expression);
            if (newExpression.IndexOf("(") == -1)
            {
                return expression.Replace("(" + newExpression + ")", Calculate(newExpression).ToString());
            }
            return expression.Replace("(" + newExpression + ")", Calculate(OpenBrackets(newExpression)).ToString());
        }
        private static string GetSubstring(string expression)
        {
            string substring = expression.Substring(expression.IndexOf("(") + 1, expression.Length - expression.IndexOf("(") - 1);
            int i = 0;
            int balance = 1;
            while (i < substring.Length)
            {
                if (substring[i] == '(') balance++;
                if (substring[i] == ')') balance--;
                if (balance == 0) break;
                i++;
            }
            return substring.Substring(0, i);
        }
        private static double Calculate(string expression)
        {
            List<string> list = Regex.Split(expression, @"(\+)|(-)|(\*)|(/)|(\^)").ToList();
            while (list.Count != 1)
            {
                Exponentiation(list);
                MultiplyAndDivide(list);
                AddAndDeduct(list);
            }
            return Convert.ToDouble(list[0]);
        }
        private static void Exponentiation(List<string> list)
        {
            int index = 0;
            while (index != -1)
            {
                index = list.IndexOf("^");
                if (index != -1)
                {
                    list[index] = Math.Pow(Convert.ToDouble(list[index - 1]), Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
        }
        private static void MultiplyAndDivide(List<string> list)
        {
            int index = 0;
            while (index != -1)
            {
                index = list.IndexOf("*");
                if (index != -1)
                {
                    list[index] = (Convert.ToDouble(list[index - 1]) * Convert.ToDouble(list[index + 1])).ToString();
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
                    list[index] = (Convert.ToDouble(list[index - 1]) / Convert.ToDouble(list[index + 1])).ToString();
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
                    list[index] = (Convert.ToDouble(list[index - 1]) + Convert.ToDouble(list[index + 1])).ToString();
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
                    list[index] = (Convert.ToDouble(list[index - 1]) - Convert.ToDouble(list[index + 1])).ToString();
                    list.RemoveAt(index + 1);
                    list.RemoveAt(index - 1);
                }
            }
        }
    }
}
