namespace CivSharp.Core.Cities
{
    class Upkeep
    {
        public int FoodUpkeep { get; set; }
        public int GoldUpkeep { get; set; }

        public Upkeep(int foodNeeded, int goldNeeded)
        {
            FoodUpkeep = foodNeeded;
            GoldUpkeep = goldNeeded;
        }
    }
}