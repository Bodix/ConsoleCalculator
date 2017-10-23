using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TO DO: 
            // 1. Сделать решения нелинейных уравнений.
            // 2. Добавить точности интерполяции методом ближайшего соседа.

            Console.Title = "Console Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
            View.GetCommand();
        }
    }
}