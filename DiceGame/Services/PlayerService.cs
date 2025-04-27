using DiceGame.Utilities;
using DiceGame.Constants;
using DiceGame.Models;


//for anything related to gathering info about players
namespace DiceGame.Services
{
    public static class PlayerService 
    {

        public static string GetPlayerName() 
        {
            Utils.Typewriter("What is your name?");
            var playerName = Console.ReadLine();
            while(string.IsNullOrEmpty(playerName)) {
                Utils.Typewriter("Invalid input. What is your name?");
                playerName = Console.ReadLine();
            }
            Utils.SpacedPrint($"Welcome to the dice table {playerName}");
            return playerName;
        }

         public static int GetNumberOfRounds()
        {
            int numberInput;
            Utils.Typewriter("How many rounds would you like to play?");
            while (!int.TryParse(Console.ReadLine(), out numberInput) || numberInput <= 0)
            {
                Utils.Typewriter("Invalid number. How many rounds would you like to play?");
            }
            return numberInput;
        }

        public static int PlayerRound(Player player) 
        {
            Utils.Typewriter($"Press {GameConstants.RollKey} to roll the dice");
            var btnPressed = Console.ReadLine();
            while (string.IsNullOrEmpty(btnPressed) || !btnPressed.ToLowerInvariant().Equals(GameConstants.RollKey)) {
                Utils.Typewriter($"Invalid input. Press {GameConstants.RollKey} to roll the dice");
                btnPressed = Console.ReadLine();
            }
            return player.Roll();
        }

    }
}