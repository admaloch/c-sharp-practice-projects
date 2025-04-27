using System;
using DiceGame.Constants;


namespace DiceGame.Utilities
{
    public static class Utils
    {
        public static void Typewriter(string message, int delay = GameConstants.DefaultDelay)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine();
        }

        public static void PrintDivider() => Console.WriteLine("-----------------");
        public static void GoodbyeMessage() => Typewriter("Thank you for playing. Restart the program if you decide to play again");

        public static void SpacedPrint(string inputString) 
        {
            Console.WriteLine();
            Typewriter(inputString);
            Console.WriteLine();
        }
        public static void PrintWithDividers(string inputString) 
        {
            PrintDivider();
            Typewriter(inputString);
            PrintDivider();
        }
    }
}
