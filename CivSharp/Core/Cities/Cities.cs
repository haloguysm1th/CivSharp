using System.Collections.Generic;
using System.Linq;
using CivSharp.Core.Cities.Belief;
using CivSharp.Core.Cities.Population;
using CivSharp.Core.Production;
using CivSharp.Interfaces;

namespace CivSharp.Core.Cities
{
    class City: ISettlement
    {
        public string Name { get; set; }

        public SettlementType SettlementType
        {
            get { return SettlementType.City; }
        }

        public int X { get; }
        public int Y { get; }

        public int GoldPerTurn { get; private set; }
        public int FoodPerTurn { get; set; }

        public int TechLevel
        {
            get
            {
                //Get the highest Tech Level building.
                return Buildings.Aggregate((firstBuilding, secondBuilding) =>
                    firstBuilding.TechLevel > secondBuilding.TechLevel ? firstBuilding : secondBuilding).TechLevel;
            }
        }

        public int TotalLoyalty
        {
            get { return CityLoyalty.Select(loyal => loyal.Amount).Sum(); }
        }

        public int TotalHappiness
        {
            get { return CityHappiness.Select(happiness => happiness.Amount).Sum(); }
        }

        public int ControllingArea { get; }
        public int VisibleArea { get; }
        public int BuildingSlots { get; }


        public CityPopulation Population { get; }

        public Civilization ControllingCivilization { get; set; }
        public Civilization FoundingCivilization { get; }
        public Beliefs FoundingBeliefs { get; }

        public CityProduction CityProduction { get; }

                public List<IBuilding> Buildings { get; }
        public List<Loyalty> CityLoyalty { get; }
        public List<Happiness> CityHappiness { get; }
        public List<TradeRoute> TradeRoutes { get; }
        public List<Outpost> Outposts { get; }
        public List<Township> Towns { get; }

    }

    //Add building slots on more unquie tiles.
    class Township : ISettlement
    {
        public int X { get; }
        public int Y { get; }
        public int VisibleArea { get; }
        public int ControllingArea { get; }
        public string Name { get; }
        public SettlementType SettlementType { get; }
    }

    /// <summary>
    /// Outposts for resupplying troops. Need a trade route between?
    /// </summary>
    class Outpost : ISettlement
    {
        public int SupplyRange { get; }
        public int X { get; }
        public int Y { get; }
        public int VisibleArea { get; }
        public int ControllingArea { get; }
        public string Name { get; }
        public SettlementType SettlementType
        {
            get { return SettlementType.Outpost; }
        }
        public int SupplyStatus { get; private set; }
        public City SupplyingCity { get; }

    }

    class TradeRoute
    {
        public City HostCity { get; }
        public City TargetCity { get; }

        public IBonus Bonus { get; }

    }
}
