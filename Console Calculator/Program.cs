using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            // TO DO: 
            // 1. Добавить точности интерполяции методом ближайшего соседа.
            // 2. Добавить проверку на сходимость в методе Ньютона.
            // 3. Изменить ввод векторов и обработку исключений при вводе векторов.

            Console.Title = "Console Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
            while (true)
                View.Calculate();
        }
    }
}