// Program.cs
using System;
using System.Dynamic;

namespace DiceGame
{
    class Program
    {
        class Person
        {
            public string Name {get; set;}
            public List<int> Rolls {get;} = new List<int>();
            public Person (string name) 
            {
                Name = name;
            }

            static Random rand = new Random();
            static int RollDie()
            {
                return rand.Next(1, 7);
            }
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
            int roundNum = 1;
            int[] playerArr = new int[5];
            int[] computerArr = new int[5];         
            GameIntro();
            while(roundNum < 6) {
                Typewriter($"Starting round {roundNum}");
                int PlayerRes = PlayerRound();
                int ComputerRes = ComputerRound();
                RoundResults(PlayerRes, ComputerRes);
                playerArr[roundNum - 1] = PlayerRes;
                computerArr[roundNum - 1] = ComputerRes;
                roundNum ++; 
            }
            FinalResults(playerArr, computerArr);
            Typewriter("Game Over: restart the program to play again");
        }


        static void GameIntro() 
        {
            Typewriter("Hello, you have entered the dice rolling game.");
        }
        static int PlayerRound() 
        {
            Typewriter("Press r to roll the dice");
            var BtnPressed = Console.ReadLine();
            while(!BtnPressed.ToLower().Equals("r")) {
                Typewriter("Invalid input. Press r to roll the dice");
                BtnPressed = Console.ReadLine();
            }
            return HandleRollDie("Player");

        }
        static int ComputerRound() 
        {
            Typewriter("Computer is rolling...");       
            return HandleRollDie("Computer");
        }
        static void RoundResults(int playerRoll, int computerRoll) 
        {
            if(playerRoll > computerRoll) {
                Typewriter("Player won! :)");
            } else if (computerRoll > playerRoll) {
                Typewriter("Computer won! :)");
            } else {
                Typewriter("Draw");
            }
        }
        static void FinalResults(int[] playerArr, int[] computerArr)
        {
            int playerRoundsWon = 0;
            int dealerRoundsWon = 0;
            for(var i = 0; i < playerArr.Length; i++)
            {
                if(playerArr[i] > computerArr[i]) playerRoundsWon++;
                else if(playerArr[i] < computerArr[i]) dealerRoundsWon++;
            }
            Typewriter($"Rounds won: {playerRoundsWon} -- Rounds lost: {dealerRoundsWon}");
            if(playerRoundsWon > dealerRoundsWon) Typewriter("Player won! :)");
            else if(playerRoundsWon < dealerRoundsWon) Typewriter("Player lost! :(");
            else Typewriter("Tie!");
        }

        static int HandleRollDie(string CurrPlayer)
        {
            int DieRoll = RollDie();
            Typewriter($"{CurrPlayer}'s Die roll is {DieRoll}");
            return DieRoll;
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

        static Random rand = new Random();
        static int RollDie()
        {
            return rand.Next(1, 7);
        }
        
    }
}
