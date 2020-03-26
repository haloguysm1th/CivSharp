using CivSharp.Core;
using CivSharp.Systems;
using RogueSharp;

namespace CivSharp.Tiles
{
    class Ocean : Tile
    {
        public Ocean(int x, int y, ICell cell) 
            : base(x, y, GraphicsItem.Ocean, Colors.OceanPrimary, Colors.OceanSecondary, cell)
        {
        }
    }
}