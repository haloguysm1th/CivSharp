using System.Collections.Generic;
using CivSharp.Interfaces;

namespace CivSharp.Core.Production
{
    class UnitProduction : IProduction<IUnit>
    {
        public int Amount { get; set; }
        public Queue<IUnit> ToProduce { get; }
    }
}