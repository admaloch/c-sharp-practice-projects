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
            Utils.SpacedPrint("Hello, you have entered the dice rolling game.");
            MenuService.DisplayMenuOptions();
            Utils.SpacedPrint("Starting new game");
            string playerName = PlayerService.GetPlayerName();
            Player player = new Player(playerName);
            bool isGameActive = true;    
            while(isGameActive) {
                Player computer = new Player("Computer");
                int numRounds = PlayerService.GetNumberOfRounds();
                int roundNum = 1;
                while(roundNum <= numRounds) {
                    System.Console.WriteLine();
                    Utils.Typewriter($"Starting round {roundNum}");
                    int playerRoll = PlayerService.PlayerRound(player);
                    int computerRoll = computer.Roll();
                    string roundWinner = GameService.GetWinner(player.Name, playerRoll, computerRoll);
                    Utils.Typewriter(roundWinner);
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
                    MenuService.DisplayMenuOptions();
                } else 
                {
                    Utils.GoodbyeMessage();
                }
            }
        }        
    }
}