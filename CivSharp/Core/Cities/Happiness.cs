namespace CivSharp.Core.Cities
{
    class Happiness
    {
        public int Amount { get; set; }
        public Civilization ThanksTo { get; }

        public Happiness(int amount, Civilization thanksTo)
        {
            Amount = amount;
            ThanksTo = thanksTo;
        }
    }
}