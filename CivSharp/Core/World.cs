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

        public IEnumerable<Tile> ConvertCellsToTiles(IEnumerable<ICell> cells)
        {
            var listOfTiles = new List<Tile>();
            foreach (var c in cells)
            {
                var tile = Tiles[c.Y, c.X];
                listOfTiles.Add(tile);
            }

            return listOfTiles;
        }

        public IEnumerable<Tile> GetTilesAlongLine(int xStart, int yStart, int xDestination, int yDestination)
        {
            var cells = GetCellsAlongLine(xStart, yStart, xDestination, yDestination);
            return ConvertCellsToTiles(cells);
        }

        public IEnumerable<Tile> GetAllTiles()
        {
            var tiles = new List<Tile>();
            foreach (var tile in Tiles)
            {
                tiles.Add(tile);
            }

            return tiles;
        }

        public IEnumerable<Tile> GetTilesInCircle(int xCenter, int yCenter, int radius)
        {
            var cells = GetCellsInCircle(xCenter, yCenter, radius);

            return ConvertCellsToTiles(cells);
        }

        public IEnumerable<Tile> GetTilesInDiamond(int xCenter, int yCenter, int distance)
        {
            var cells = GetCellsInDiamond(xCenter, yCenter, distance);

            return ConvertCellsToTiles(cells);
        }

        public IEnumerable<Tile> GetBorderTilesInSquare(int xCenter, int yCenter, int distance)
        {
            var cells = GetBorderCellsInSquare(xCenter, yCenter, distance);

            return ConvertCellsToTiles(cells);
        }

        public IEnumerable<Tile> GetBorderTilesInCircle(int xCenter, int yCenter, int radius)
        {
            var cells = GetBorderCellsInCircle(xCenter, yCenter, radius);

            return ConvertCellsToTiles(cells);
        }

        public IEnumerable<Tile> GetBorderTilesInDiamond(int xCenter, int yCenter, int distance)
        {
            var cells = GetBorderCellsInDiamond(xCenter, yCenter, distance);

            return ConvertCellsToTiles(cells);
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
