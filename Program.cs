// Program.cs
using System;
using System.Dynamic;

namespace DiceGame
{
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
                int diceNum = rand.Next(1, 7);
                Rolls.Add(diceNum);
                Typewriter($"{Name} rolled a {diceNum}");
                return diceNum;
            }

        }
        
        static void Main(string[] args)
        {   
           
                        
            Typewriter("Hello, you have entered the dice rolling game.");

            string playerName = PlayerName();
            Player player = new Player(playerName);
            Player computer = new Player("Computer");

            int numRounds = NumRounds();
            int roundNum = 1;

            while(roundNum <= numRounds) {
                System.Console.WriteLine("----------");
                Typewriter($"Starting round {roundNum}");
                int playerRoll = PlayerRound(player);
                int computerRoll = computer.Roll();
                RoundResults(playerRoll, computerRoll);
                roundNum ++; 
            }
            FinalResults(player, computer);
            Typewriter("Game Over: restart the program to play again");
        }


        static string PlayerName() 
        {
            Typewriter("What is your name?");
            var playerName = Console.ReadLine();
            while(string.IsNullOrEmpty(playerName)) {
                Typewriter("Invalid input. What is your name?");
                playerName = Console.ReadLine();
            }
            Typewriter($"Welcome to the dice table {playerName}");
            return playerName;
        }

        static int NumRounds() 
        {
            Typewriter("How many rounds would you like to play?");
            bool isNumber = int.TryParse(Console.ReadLine(), out int number);
            while(!isNumber) {
                Typewriter("Invalid number. How many rounds would you like to play?");
                number = Convert.ToInt32(Console.ReadLine());
            }
            return number;
        }

        static int PlayerRound(Player player) 
        {
            Typewriter("Press r to roll the dice");
            var BtnPressed = Console.ReadLine();
            while(!BtnPressed.ToLower().Equals("r")) {
                Typewriter("Invalid input. Press r to roll the dice");
                BtnPressed = Console.ReadLine();
            }
            return player.Roll();
        }
        static void RoundResults(int playerRoll, int computerRoll) 
        {
            if(playerRoll > computerRoll) {
                Typewriter("Player won! :)");
            } else if (computerRoll > playerRoll) {
                Typewriter("Computer won! :(");
            } else {
                Typewriter("Draw");
            }
        }
        static void FinalResults(Player player, Player computer)
        {
            int playerRoundsWon = 0;
            int dealerRoundsWon = 0;
            System.Console.WriteLine("----------");
            Typewriter("All rounds complete. Showing final results...");
            
            for(var i = 0; i < player.Rolls.Count; i++)
            {
                if(player.Rolls[i] > computer.Rolls[i]) playerRoundsWon++;
                else if(player.Rolls[i] < computer.Rolls[i]) dealerRoundsWon++;
            }

            Typewriter($"Rounds won: {playerRoundsWon} -- Rounds lost: {dealerRoundsWon}");

            if(playerRoundsWon > dealerRoundsWon) Typewriter("Player won! :)");
            else if(playerRoundsWon < dealerRoundsWon) Typewriter("Player lost! :(");
            else Typewriter("Tie!");
        }

        static void Typewriter(string message, int delay = 20)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.WriteLine(); // To move to the next line
        }

        
    }
}
