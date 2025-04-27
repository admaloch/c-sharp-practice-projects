using DiceGame.Enums;
using DiceGame.Utilities;
using DiceGame.Models;
using DiceGame.Constants;


//for anything related to handling main game play
namespace DiceGame.Services
{
    public static class GameService 
    {
        public static void GameIntro()
        {
            System.Console.WriteLine();
            Utils.PrintDivider();
            Utils.Typewriter("Starting a new game");
        }
        public static (int playerRoundsWon, int computerRoundsWon, int tiedRounds) 
            CalculateRoundResults(Player player, Player computer)
        {
            int playerRoundsWon = 0;
            int computerRoundsWon = 0;
            int roundsTied = 0;
            Utils.SpacedPrint("All rounds complete. Showing final results...");
            for(var i = 0; i < player.Rolls.Count; i++)
            {
                if(player.Rolls[i] > computer.Rolls[i]) playerRoundsWon++;
                else if(player.Rolls[i] < computer.Rolls[i]) computerRoundsWon++;
                else roundsTied++;
            }
            Utils.Typewriter($"Won: {playerRoundsWon} -- Lost: {computerRoundsWon} -- Tied: {roundsTied} ");
            string roundWinner = GetWinner(player.Name, playerRoundsWon, computerRoundsWon);
            Utils.Typewriter(roundWinner);
            return (playerRoundsWon, computerRoundsWon, roundsTied);
        }



        public static string GetWinner(string playerName, int playerNum, int computerNum)
        {
            if(playerNum > computerNum) return $"{playerName} wins!";
            else if(playerNum < computerNum) return "Computer wins!";
            else return "Draw!";
        }

        public static bool IsGameOver()
        {
            Utils.PrintWithDividers($"Game Over: Press {GameConstants.PlayAgainKey} to play again or {GameConstants.QuitKey} to quit.");
            var btnPressed = Console.ReadLine();
            while (string.IsNullOrEmpty(btnPressed) 
                || !btnPressed.ToLowerInvariant().Equals(GameConstants.PlayAgainKey) 
                && !btnPressed.ToLowerInvariant().Equals(GameConstants.QuitKey)) {
                    Utils.Typewriter($"Invalid input. Press {GameConstants.PlayAgainKey} to play again or {GameConstants.QuitKey}");
                    btnPressed = Console.ReadLine();
            }
            bool playGameAgain = btnPressed.ToLowerInvariant().Equals(GameConstants.PlayAgainKey) 
                ? true : false;
            return playGameAgain;
        }
        
    }
}

