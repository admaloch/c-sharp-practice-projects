// Program.cs
using System;

namespace DiceGame
{
    public enum GameResult
        {
            PlayerWin,
            ComputerWin,
            Draw
        }
    public static class Constants
        {
            public const int MinDiceValue = 1;
            public const int MaxDiceValue = 6;
            public const int DefaultDelay = 20;
            public const string RollKey = "r";
        }
        public static class Utils
        {
            public static void Typewriter(string message, int delay = Constants.DefaultDelay)
            {
                foreach (char c in message)
                {
                    Console.Write(c);
                    Thread.Sleep(delay);
                }
                Console.WriteLine();
            }
        public static void PrintDivider() => Console.WriteLine("--------------------");

        }
    class Program
    {
        class Player
        {
            public string Name {get; set;}
            public List<int> Rolls {get;} = new List<int>();
            public Player (string name) 
            {
                Name = name;
            }
            static Random rand = new Random();
            public int Roll()
            {
                int diceNum = rand.Next(Constants.MinDiceValue, Constants.MaxDiceValue + 1);
                Rolls.Add(diceNum);
                Utils.Typewriter($"{Name} rolled a {diceNum}");
                return diceNum;
            }
        }
        static void Main(string[] args)
        {       
            Utils.PrintDivider();                 
            Utils.Typewriter("Hello, you have entered the dice rolling game.");
            string playerName = PlayerName();
            Player player = new Player(playerName);
            Player computer = new Player("Computer");
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
            FinalResults(player, computer);
            Utils.Typewriter("Game Over: restart the program to play again");
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
            int number;
            Utils.Typewriter("How many rounds would you like to play?");
            while (!int.TryParse(Console.ReadLine(), out number) || number <= 0)
            {
                Utils.Typewriter("Invalid number. How many rounds would you like to play?");
            }
            return number;
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
        static void FinalResults(Player player, Player computer)
        {
            int playerRoundsWon = 0;
            int computerRoundsWon = 0;
            Utils.PrintDivider();
            Utils.Typewriter("All rounds complete. Showing final results...");
            
            for(var i = 0; i < player.Rolls.Count; i++)
            {
                if(player.Rolls[i] > computer.Rolls[i]) playerRoundsWon++;
                else if(player.Rolls[i] < computer.Rolls[i]) computerRoundsWon++;
            }

            Utils.Typewriter($"Rounds won: {playerRoundsWon} -- Rounds lost: {computerRoundsWon}");
            
            GameResult roundResult = GetResult(playerRoundsWon, computerRoundsWon);
            PrintWinner(roundResult, player.Name);
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
    }
}