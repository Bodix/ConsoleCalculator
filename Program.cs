using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TO DO: 
            // 1. Починить решение СЛАУ методом итераций. +++
            // 2. Сделать решение СЛАУ методом Гаусса. +++
            // 3. Сделать решения нелинейных уравнений.
            // 4. Сделать запись лога в файл. +++

            Console.Title = "Console Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
            View.GetCommand();
        }
    }
}