using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;

namespace CivSharp.Core
{
    class World : Map
    {
        public Tile[,] Tiles { get; private set; }

        public World()
        {
        }

        public new void Initialize(int width, int height)
        {
            base.Initialize(width,height);

            Tiles = new Tile[Height,Width];
        }

        public void SetTileProperty(Tile tile, int x, int y, bool transparent, bool walkable, bool explored)
        {
            Tiles[y, x] = tile;
            SetCellProperties(x,y,transparent,walkable,explored);
        }

        public IEnumerable<Tile> GetTiles(IEnumerable<ICell> cells)
        {
            var tiles = new List<Tile>();
            foreach (var cell in cells)
            {
                var tile = Tiles[cell.Y,cell.X];
                tiles.Add(tile);
            }

            return tiles;
        }

        public void Update()
        {

        }

        public void Draw(RLConsole mapConsole)
        {
            foreach (var t in Tiles)
            {
                t.Draw(mapConsole,this);
            }
        }
    }
}
