namespace CivSharp.Core.Production
{
    class CityProduction
    {
        public int TotalProduction
        {
            get { return UnitProduction.Amount + BuildingProduction.Amount; }
        }

        public UnitProduction UnitProduction { get; }
        public BuildingProduction BuildingProduction { get; }
    }
}