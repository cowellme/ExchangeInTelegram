using System;

namespace ConsoleAppTG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TelegramDriver.Start();
            Console.ReadLine();
        }
    }
}
