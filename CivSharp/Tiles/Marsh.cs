using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Tiles
{
    class Marsh : Tile
    {
        public Marsh(int x, int y, ICell cell) :
            base(x, y, false, true, 0, 0, 0, null,
                GraphicsItem.Marsh, Colors.MarshPrimary, Colors.MarshSecondary, cell)
        {
        }
    }
}
