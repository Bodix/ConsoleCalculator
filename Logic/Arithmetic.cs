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
            if (expression.IndexOf('(') == -1)
                return Calculate(expression).ToString();
            while (expression.IndexOf('(') != -1)
            {
                string newExpression = GetSubstring(expression);
                expression = expression.Replace("(" + newExpression + ")", OpenBrackets(newExpression));
            }
            return Calculate(expression).ToString();
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
            List<string> list = Regex.Split(expression, @"(\+|(?<=\d *)-|\*|/|\^)").ToList();
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
            int i = 0;
            while (i != -1)
            {
                i = list.IndexOf("^");
                if (i != -1)
                {
                    list[i] = Math.Pow(Convert.ToDouble(list[i - 1]), Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                }
            }
        }
        private static void MultiplyAndDivide(List<string> list)
        {
            int i = 0;
            while (i != -1)
            {
                i = list.IndexOf("*");
                if (i != -1)
                {
                    list[i] = (Convert.ToDouble(list[i - 1]) * Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                }
            }
            i = 0;
            while (i != -1)
            {
                i = list.IndexOf("/");
                if (i != -1)
                {
                    list[i] = (Convert.ToDouble(list[i - 1]) / Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                }
            }
        }
        private static void AddAndDeduct(List<string> list)
        {
            int i = 0;
            while (i != -1)
            {
                i = list.IndexOf("+");
                if (i != -1)
                {
                    list[i] = (Convert.ToDouble(list[i - 1]) + Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                }
            }
            i = 0;
            while (i != -1)
            {
                i = list.IndexOf("-");
                if (i != -1)
                {
                    list[i] = (Convert.ToDouble(list[i - 1]) - Convert.ToDouble(list[i + 1])).ToString();
                    list.RemoveAt(i + 1);
                    list.RemoveAt(i - 1);
                }
            }
        }
    }
}
