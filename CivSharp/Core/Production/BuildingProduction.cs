using System.Collections.Generic;
using CivSharp.Interfaces;

namespace CivSharp.Core.Production
{
    class BuildingProduction : IProduction<IBuilding>
    {
        public int Amount { get; }
        public Queue<IBuilding> ToProduce { get; }
    }
}