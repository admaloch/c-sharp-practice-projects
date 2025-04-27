//for anything related to generating user/game statistics from json file
using DiceGame.Utilities;
using DiceGame.Enums;
using DiceGame.Models;
using DiceGame.Constants;

namespace DiceGame.Services
{
    public static class StatisticsService 
    {
        public static void ViewGameHistory()
        {
            List<GameStats> gameData = FileManager.LoadFromJsonFile<GameStats>(FileLocations.GameStatsLocation);

            if (gameData.Count == 0)
            {
                Utils.Typewriter("No player data found");
                return;
            }

            foreach (var item in gameData)
            {
                var roundResult = GameService.GetWinner(item.PlayerName,item.PlayerRoundsWon, item.ComputerRoundsWon);
                Console.WriteLine($""" 

                    Date: {item.DatePlayed}
                    Name: {item.PlayerName}
                    Rounds Won: {item.PlayerRoundsWon}
                    Rounds Lost: {item.ComputerRoundsWon}
                    Rounds Tied: {item.TiedRounds}
                    {roundResult}

                """);
                
            }
        }
       
        public static void ViewPlayerStats()
        {
            List<GameStats> gameData = FileManager.LoadFromJsonFile<GameStats>(FileLocations.GameStatsLocation);

            if(gameData.Count == 0) {
                Utils.Typewriter("No player data found");
                return;
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
        public static void ResetStats()
        {
            File.WriteAllText(FileLocations.GameStatsLocation, "[]");
            Utils.Typewriter("Game Data has been reset");
        }
    }
}