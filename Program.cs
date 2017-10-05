using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TO DO: 
            // 1. Починить СЛАУ методом итераций.
            // 2. Сделать решения нелинейных уравнений.

            Console.Title = "Console Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
            View.Command();
        }
    }
}