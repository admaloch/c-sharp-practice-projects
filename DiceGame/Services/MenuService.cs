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

                Utils.Typewriter($"""

                === Dice Game Menu ===
                1. Start New Game
                2. View Game History
                3. View Player Stats
                4. Reset Stats
                5. Exit

                Select an option: 
                """, 8);  
                
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
                        StatisticsService.ResetStats();
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