using CivSharp.Core;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Tiles
{
    class Mountain : Tile
    {
        public Mountain(int x, int y, ICell cell) 
            : base(x, y, GraphicsItem.Mountains, Colors.RockPrimary, Colors.RockSecondary, cell)
        {
        }
    }
}