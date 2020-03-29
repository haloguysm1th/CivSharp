namespace CivSharp.Core.Cities
{
    class Loyalty
    {
        public int Amount { get; }
        public Civilization CivlizationLoyalTo { get; }

        public Loyalty(int amount, Civilization civlizationLoyalTo)
        {
            Amount = amount;
            CivlizationLoyalTo = civlizationLoyalTo;
        }
    }
}