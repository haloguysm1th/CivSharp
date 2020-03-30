using CivSharp.Core;
using CivSharp.Core.Cities;

namespace CivSharp.Interfaces
{
    interface IBonus
    {
        int FoodBonus { get; }
        int ProductionBonus { get; }
        int GoldBonus { get; }
        Loyalty LoyaltyBonus { get; }
        Happiness HappinessBonus { get; }
    }
}