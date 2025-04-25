//for anything related to generating user/game statistics from json file
using DiceGame.Utilities;
using DiceGame.Enums;
using DiceGame.Models;

namespace DiceGame.Services
{
    public static class StatisticsService 
    {
        public static void ViewGameHistory()
        {
            List<GameStats> gameData = FileManager.LoadFromJsonFile<GameStats>("gamestats.json");

            if (gameData.Count == 0)
            {
                Utils.Typewriter("No player data found");
                return;
            }

            foreach (var item in gameData)
            {
                Console.WriteLine($""" 

                    Date: {item.DatePlayed}
                    Name: {item.PlayerName}
                    ounds Won: {item.PlayerRoundsWon}
                    Rounds Lost: {item.ComputerRoundsWon}
                    Rounds Tied: {item.TiedRounds}
                """);
                
                var roundResult = GameService.GetWinner(item.PlayerRoundsWon, item.ComputerRoundsWon);
                GameService.PrintWinner(roundResult, item.PlayerName);
                Console.WriteLine();
            }
        }
       
        public static void ViewPlayerStats()
        {
            // List<GameStats> history = GetGameData("gamestats.json");
            List<GameStats> gameData = FileManager.LoadFromJsonFile<GameStats>("gamestats.json");

            if(gameData.Count > 0) {
                                Utils.Typewriter("No player data found");
            }

            var groupedByName = gameData.GroupBy(item => item.PlayerName);
            foreach(var group in groupedByName)
            {
                int totalRoundsWon = group.Sum(g => g.PlayerRoundsWon);
                int totalRoundsLost = group.Sum(g => g.ComputerRoundsWon);
                int totalRoundsTied = group.Sum(g => g.TiedRounds);
                int totalRounds = totalRoundsWon + totalRoundsLost + totalRoundsTied;
                double winPercentage = totalRounds > 0 ? (double)totalRoundsWon / totalRounds * 100 : 0;
                Console.WriteLine($"""

                    Player: {group.Key}
                    -------------------------
                    Games Played: {group.Count()}
                    Total Rounds Played: {group.Sum(g => g.PlayerRoundsWon + g.ComputerRoundsWon + g.TiedRounds)}
                    --- Won: {totalRoundsWon}
                    --- Lost: {totalRoundsLost}
                    --- Tied: {totalRoundsTied}
                    Win Percentage: {winPercentage:F2}%

                """);                 
            }
        }
    }
}