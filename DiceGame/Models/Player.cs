using c_app.Constants;
using DiceGame.Utilities;

namespace DiceGame.Models
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
}

