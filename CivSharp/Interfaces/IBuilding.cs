using CivSharp.Core;
using CivSharp.Core.Cities;

namespace CivSharp.Interfaces
{
    interface IBuilding
    {
        int GoldCost { get; }
        int ProductionCost { get; }
        int GoldUpkeep { get; }
        int ProductionUpKeep { get; }

        int TechLevel { get; }
        Job Jobs { get; }

        void AdvanceBuildingToNextLevel();

    }
}