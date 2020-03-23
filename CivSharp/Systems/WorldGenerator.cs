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

            return _world;
        }
    }
}
