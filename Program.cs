using System;

namespace ConsoleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Console Calculator";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\"Console Calculator\" by Bogdan Nikolaev (IT-36a, NTU \"KhPI\")");
            View.Calculate();
        }
    }
}