using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CivSharp.Interfaces
{
    enum SettlementType
    {
        City, //large city, full production queue and build slots
        Outpost, //Resupplies military units, expands controlling area andvisile area
        Township //adds build slots, expands controlling and visible areas.

    }
    interface ISettlement
    {

        int X { get; }
        int Y { get; }
        int VisibleArea { get; }
        int ControllingArea { get; }
        string Name { get; }
        SettlementType SettlementType { get; }
    }
}
