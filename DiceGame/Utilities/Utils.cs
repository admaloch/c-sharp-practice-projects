using System;
using System.Threading;
using c_app.Constants;

namespace DiceGame.Utilities
{
    public static class Utils
    {
        public static void Typewriter(string message, int delay = Constants.DefaultDelay)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void PrintDivider() => Console.WriteLine("--------------------");
    }
}
