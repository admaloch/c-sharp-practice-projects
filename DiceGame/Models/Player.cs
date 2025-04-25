using DiceGame.Constants;
using DiceGame.Utilities;

namespace DiceGame.Models
{
    public class Player
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
            int diceNum = rand.Next(GameConstants.MinDiceValue, GameConstants.MaxDiceValue + 1);
            Rolls.Add(diceNum);
            Utils.Typewriter($"{Name} rolled a {diceNum}");
            return diceNum;
        }
    } 
}

