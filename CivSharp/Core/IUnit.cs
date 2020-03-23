using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Interfaces;

namespace CivSharp.Core
{
    interface IUnit : IDrawable
    {
        Tile CurrentTile { get; }
        int Speed { get; }
        int health { get; }

        void MoveTile(Tile targetTile);
        void UseAbility(IAbility ability);
    }
}
