using CivSharp.Core.Cities.Belief;

namespace CivSharp.Core.Cities.Population
{
    class Population
    {
        public Civilization Civilization { get; }
        public Upkeep Upkeep { get; }
        public Beliefs Beliefs { get; }
        public PopulationType PopulationType { get; set; }

        public Population(Civilization civlization, Upkeep upkeep, Beliefs beliefs, PopulationType startingType)
        {
            Civilization = civlization;
            Upkeep = upkeep;
            Beliefs = beliefs;
            PopulationType = startingType;
        }
    }
}