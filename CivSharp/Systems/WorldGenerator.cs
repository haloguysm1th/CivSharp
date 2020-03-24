using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core;
using CivSharp.Tiles;

namespace CivSharp.Systems
{
    class WorldGenerator
    {
        private int _width;
        private int _height;
        private World _world;

        public WorldGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _world = new World();
        }

        public World GenerateWorld()
        {
            _world.Initialize(_width,_height);
            var rand = new Random();
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    //TODO Change isexplored to false. For now it is set to true for testing!
                    if (rand.Next(0, 7) < 6)
                    {
                        var plains = new Plains(x, y, _world.GetCell(x, y));
                        _world.SetTileProperty(plains, x, y, false, true, true);
                    }
                    else
                    {
                        var mount = new Mountain(x,y,_world.GetCell(x,y));
                        _world.SetTileProperty(mount,x,y,false,true,true);
                    }
                }
            }

            //Set all the edge tiles to water
            var topCells = _world.GetCellsAlongLine(0, 0, _width-1, 0);
            var bottomCells = _world.GetCellsAlongLine(0, _height, _width-1, _height-1);
            var LeftCells = _world.GetCellsAlongLine(0, 0, 0, _height);
            var RightCells = _world.GetCellsAlongLine(_width, 0, _width-1, _height-1);

            var topTiles = _world.GetTiles(topCells);
            var bottomTiles = _world.GetTiles(bottomCells);
            var leftTiles = _world.GetTiles(LeftCells);
            var rightTiles = _world.GetTiles(RightCells);

            var tiles = new List<Tile>();
            tiles.AddRange(topTiles);
            tiles.AddRange(bottomTiles);
            tiles.AddRange(leftTiles);
            tiles.AddRange(rightTiles);

            foreach (var t in tiles)
            {
                _world.Tiles[t.Y,t.X] = new Ocean(t.X,t.X,t.Cell);

            }

            return _world;
        }
    }
}
