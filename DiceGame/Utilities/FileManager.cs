using System.Text.Json;
using DiceGame.Models;


namespace DiceGame.Utilities
{
    public static class FileManager
    {
        private static string filePath = "gamestats.json";

        public static void SaveGameStats(GameStats stats)
        {
            List<GameStats> allStats = new();

            // Load existing data if it exists
            if (File.Exists(filePath))
            {
                string existingData = File.ReadAllText(filePath);
                if (!string.IsNullOrWhiteSpace(existingData))
                {
                    allStats = JsonSerializer.Deserialize<List<GameStats>>(existingData) ?? new List<GameStats>();
                }
            }

            allStats.Add(stats); // Add the new game result
            string json = JsonSerializer.Serialize(allStats, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

         public static List<T> LoadFromJsonFile<T>(string filePath)
            {
                if (!File.Exists(filePath))
                {
                    return new List<T>();
                }

                string json = File.ReadAllText(filePath);
                List<T>? data = JsonSerializer.Deserialize<List<T>>(json);
                return data ?? new List<T>();
            }
    }
}
