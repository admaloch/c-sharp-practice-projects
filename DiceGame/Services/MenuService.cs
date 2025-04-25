//for anything related to displying menu options
using DiceGame.Constants;
using DiceGame.Utilities;

namespace DiceGame.Services
{
    public static class MenuService 
    {
        public static void DisplayMenuOptions()
        {
            Utils.Typewriter("Select one from the following options");
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("=== Dice Game Menu ===");
                Console.WriteLine($"1. Start New Game");
                Console.WriteLine($"2. View Game History");
                Console.WriteLine($"3. View Player Stats");
                Console.WriteLine($"4. Reset Stats");
                Console.WriteLine($"5. Exit");
                Console.Write("Select an option: ");
                
                var choice = Console.ReadLine();
                
                switch (choice)
                {
                    case MainMenuVars.StartNewGame:
                        exit = true;
                        // StartNewGame();
                        break;
                    case MainMenuVars.ViewGameHistory:
                        StatisticsService.ViewGameHistory();
                        break;
                    case MainMenuVars.ViewPlayerStats:
                        StatisticsService.ViewPlayerStats();
                        break;
                    case MainMenuVars.ResetStats:
                        // ResetStats();
                        break;
                    case MainMenuVars.Exit:
                        Utils.GoodbyeMessage();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid selection. Please try again.");
                        break;
                }
            }
        }
    }
}