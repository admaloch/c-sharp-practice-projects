namespace DiceGame.Models
{
    public class GameStats
    {
    public string PlayerName { get; set; } = string.Empty;
    public int PlayerRoundsWon { get; set; }
    public int ComputerRoundsWon { get; set; }
    public int TiedRounds { get; set; }
    public DateTime DatePlayed { get; set; }
    }
}