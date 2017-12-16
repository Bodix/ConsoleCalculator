using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TO DO: 
            // 1. Добавить проверки на сходимость в методах решений нелинейных уравнений.
            // 2. Интегрирование.
            // 3. Дифференциальные уравнения.

            Console.Title = "Console Calculator";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
                View.Calculate();
        }
    }
}