using DiceGame.Enums;
using DiceGame.Utilities;
using DiceGame.Models;
using DiceGame.Constants;


//for anything related to handling main game play
namespace DiceGame.Services
{
    public static class GameService 
    {
        public static (int playerRoundsWon, int computerRoundsWon, int tiedRounds) 
            CalculateRoundResults(Player player, Player computer)
        {
            int playerRoundsWon = 0;
            int computerRoundsWon = 0;
            int roundsTied = 0;
            Utils.PrintDivider();
            Utils.Typewriter("All rounds complete. Showing final results...");
            for(var i = 0; i < player.Rolls.Count; i++)
            {
                if(player.Rolls[i] > computer.Rolls[i]) playerRoundsWon++;
                else if(player.Rolls[i] < computer.Rolls[i]) computerRoundsWon++;
                else roundsTied++;
            }
            Utils.Typewriter($"Won: {playerRoundsWon} -- Lost: {computerRoundsWon} -- Tied: {roundsTied} ");
            GameResult roundResult = GetWinner(playerRoundsWon, computerRoundsWon);
            PrintWinner(roundResult, player.Name);
            return (playerRoundsWon, computerRoundsWon, roundsTied);
        }

        public static GameResult GetWinner(int playerNum, int computerNum)
        {
            if(playerNum > computerNum) return GameResult.PlayerWin;
            else if (computerNum > playerNum)  return GameResult.ComputerWin;
            else return GameResult.Draw;
        }

        public static void PrintWinner(GameResult result, string playerName) 
        {
           switch (result)
           {
                case GameResult.PlayerWin:
                    Utils.Typewriter($"Winner: {playerName}");
                    break;
                case GameResult.ComputerWin:
                    Utils.Typewriter("Winner: Computer");
                    break;
                case GameResult.Draw:
                    Utils.Typewriter("Draw!");
                    break;
           }
        }

        public static bool IsGameOver()
        {
            Utils.Typewriter($"Game Over: Press {GameConstants.PlayAgainKey} to play again or {GameConstants.QuitKey} to quit.");
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

