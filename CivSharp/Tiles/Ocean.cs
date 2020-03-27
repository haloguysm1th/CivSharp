using CivSharp.Core;
using CivSharp.Systems;
using RogueSharp;

namespace CivSharp.Tiles
{
    class Ocean : Tile
    {
        public Ocean(int x, int y, ICell cell) 
            : base(x, y, false, true, 0, 0, 0, null,
                GraphicsItem.Ocean, Colors.OceanPrimary, Colors.OceanSecondary, cell)
        {
        }
    }
}