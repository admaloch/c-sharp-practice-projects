// Program.cs
using System;
using DiceGame.Enums;
using DiceGame.Utilities;
using c_app.Constants;
using DiceGame.Models;

namespace DiceGame
{
    class Program
    {
        static void Main(string[] args)
        {       
            Utils.PrintDivider();                 
            Utils.Typewriter("Hello, you have entered the dice rolling game.");
            string playerName = PlayerName();
            Player player = new Player(playerName);
            Player computer = new Player("Computer");
            bool isGameActive = true;
            while(isGameActive) {
                int numRounds = NumRounds();
                int roundNum = 1;
                while(roundNum <= numRounds) {
                    Utils.PrintDivider();
                    Utils.Typewriter($"Starting round {roundNum}");
                    int playerRoll = PlayerRound(player);
                    int computerRoll = computer.Roll();
                    GameResult roundResult = GetResult(playerRoll, computerRoll);
                    PrintWinner(roundResult, player.Name);
                    roundNum ++; 
                }
                var (playerWins, computerWins, ties) = CalculateRoundResults(player, computer);
                GameStats stats = new GameStats
                {
                    PlayerName = player.Name,
                    PlayerRoundsWon = playerWins,
                    ComputerRoundsWon = computerWins,
                    TiedRounds = ties,
                    DatePlayed = DateTime.Now
                };
                
                FileManager.SaveGameStats(stats);
                isGameActive = GameOver();

                if (isGameActive)
                {
                    player.Rolls.Clear();
                    computer.Rolls.Clear();
                } else 
                {
                    Utils.Typewriter("Thank you for playing. Restart the program if you decide to play again");
                }
            }
        }
        static string PlayerName() 
        {
            Utils.Typewriter("What is your name?");
            var playerName = Console.ReadLine();
            while(string.IsNullOrEmpty(playerName)) {
                Utils.Typewriter("Invalid input. What is your name?");
                playerName = Console.ReadLine();
            }
            Utils.Typewriter($"Welcome to the dice table {playerName}");
            return playerName;
        }
        static int NumRounds()
        {
            int numberInput;
            Utils.Typewriter("How many rounds would you like to play?");
            while (!int.TryParse(Console.ReadLine(), out numberInput) || numberInput <= 0)
            {
                Utils.Typewriter("Invalid number. How many rounds would you like to play?");
            }
            return numberInput;
        }
        static int PlayerRound(Player player) 
        {
            Utils.Typewriter($"Press {Constants.RollKey} to roll the dice");
            var btnPressed = Console.ReadLine();
            while (string.IsNullOrEmpty(btnPressed) || !btnPressed.ToLowerInvariant().Equals(Constants.RollKey)) {
                Utils.Typewriter($"Invalid input. Press {Constants.RollKey} to roll the dice");
                btnPressed = Console.ReadLine();
            }
            return player.Roll();
        }
        static 
            (int playerRoundsWon, int computerRoundsWon, int tiedRounds) 
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
            
            GameResult roundResult = GetResult(playerRoundsWon, computerRoundsWon);
            PrintWinner(roundResult, player.Name);
            return (playerRoundsWon, computerRoundsWon, roundsTied);

        }
        static GameResult GetResult(int playerNum, int computerNum)
        {
            if(playerNum > computerNum) return GameResult.PlayerWin;
            else if (computerNum > playerNum)  return GameResult.ComputerWin;
            else return GameResult.Draw;
        }
        static void PrintWinner(GameResult result, string playerName) 
        {
           switch (result)
           {
                case GameResult.PlayerWin:
                    Utils.Typewriter($"{playerName} won! :)");
                    break;
                case GameResult.ComputerWin:
                    Utils.Typewriter("The computer won! :(");
                    break;
                case GameResult.Draw:
                    Utils.Typewriter("Draw!");
                    break;
           }
        }
        static bool GameOver()
        {
            Utils.Typewriter($"Game Over: Press {Constants.PlayAgainKey} to play again or {Constants.QuitKey} to quit.");
            var btnPressed = Console.ReadLine();
            while (string.IsNullOrEmpty(btnPressed) 
                || !btnPressed.ToLowerInvariant().Equals(Constants.PlayAgainKey) 
                && !btnPressed.ToLowerInvariant().Equals(Constants.QuitKey)) {
                    Utils.Typewriter($"Invalid input. Press {Constants.PlayAgainKey} to play again or {Constants.QuitKey}");
                    btnPressed = Console.ReadLine();
            }
            bool playGameAgain = btnPressed.ToLowerInvariant().Equals(Constants.PlayAgainKey) 
                ? true : false;
            return playGameAgain;
        }
    }
}