// Program.cs
using System;
using DiceGame.Enums;
using DiceGame.Utilities;
using DiceGame.Models;
using DiceGame.Services;

namespace DiceGame
{
    class Program
    {
        static void Main(string[] args)
        {   
            bool isGameActive = false;    
            Utils.PrintDivider();                 
            Utils.Typewriter("Hello, you have entered the dice rolling game.");
            MenuService.DisplayMenuOptions();
            while(isGameActive) {
                string playerName = PlayerService.GetPlayerName();
                Player player = new Player(playerName);
                Player computer = new Player("Computer");
                int numRounds = PlayerService.GetNumberOfRounds();
                int roundNum = 1;
                while(roundNum <= numRounds) {
                    Utils.PrintDivider();
                    Utils.Typewriter($"Starting round {roundNum}");
                    int playerRoll = PlayerService.PlayerRound(player);
                    int computerRoll = computer.Roll();
                    GameResult roundResult = GameService.GetWinner(playerRoll, computerRoll);
                    GameService.PrintWinner(roundResult, player.Name);
                    roundNum ++; 
                }
                var (playerWins, computerWins, ties) = 
                    GameService.CalculateRoundResults(player, computer);
                GameStats stats = new GameStats
                {
                    PlayerName = player.Name,
                    PlayerRoundsWon = playerWins,
                    ComputerRoundsWon = computerWins,
                    TiedRounds = ties,
                    DatePlayed = DateTime.Now
                };
                
                FileManager.SaveGameStats(stats);
                isGameActive = GameService.IsGameOver();

                if (isGameActive)
                {
                    player.Rolls.Clear();
                    computer.Rolls.Clear();
                } else 
                {
                    Utils.GoodbyeMessage();
                }
            }
        }        
    }
}