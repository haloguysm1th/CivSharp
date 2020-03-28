using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core;
using CivSharp.Tiles;
using RogueSharp;

namespace CivSharp.Systems
{

    class WorldGenerator
    {
        private int _width;
        private int _height;
        private World _world;
        private static readonly int ISLAND_MIN_SIZE_NORMAL = 20;
        private static readonly int ISLAND_MAX_SIZE_NORMAL = 60;
        private static readonly int ISLAND_MIN_SIZE_LARGE_CONTINENT = 40;
        private static readonly int ISLAND_MAX_SIZE_LARGE_CONTINENT = 80;

        public WorldGenerator(int width, int height)
        {
            _width = width;
            _height = height;
            _world = new World();
        }
        public void GenerateOceans()
        {
            var tiles = _world.GetTilesAlongLine(0, 0, _width, 0) as List<Tile>;
            tiles.AddRange(_world.GetTilesAlongLine(0,_height, _width, _height));
            tiles.AddRange(_world.GetTilesAlongLine(0,0,0,_height));
            tiles.AddRange(_world.GetTilesAlongLine(_width,0,_width,_height));

            foreach (var tile in tiles)
            {
                _world.Tiles[tile.Y,tile.X] = new Ocean(tile.X,tile.Y,tile.Cell);
            }
        }

        private void CreateWorldOcean()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    //ocean tiles shouldn't be able to see though to the next beacuse reasons.
                    var ocean = new Ocean(x,y,_world.GetCell(x,y));
                    _world.SetTileProperty(ocean,x,y,false,true,false);
                }
            }
        }

        private List<Tile> GetTilesInRange(Rectangle rect)
        {
            var tile = new List<Tile>();
            for (int x = rect.Left; x < rect.Right; x++)
            {
                for (int y = rect.Top ; y < rect.Bottom; y++)
                {
                    if (!(y > _height-1 || y < 0 || x > _width-1 || x < 0))
                    {
                        tile.Add(_world.Tiles[y, x]);
                    }
                }
            }
            return tile;
        }

        private void GenerateRectangleIsland(Rectangle island)
        {
            var random =  new Random();
            int distance = 1;
            var tiles = _world.GetTilesInRectangle(island);
            foreach (var tile in tiles)
            {
                //
                if (tile.Y > _world.Height - 5)
                {
                    var winter = new Tundra(tile.X,tile.Y,tile.Cell);
                    _world.SetTileProperty(winter,tile.X,tile.Y,true,true,false);
                }else if (tile.Y > _world.Height - 10)
                {
                    var desert = new Dessert(tile.X, tile.Y, tile.Cell);
                    _world.SetTileProperty(desert, tile.X, tile.Y, true, true, false);
                } else if (tile.Y < 5)
                {
                    var winter = new Tundra(tile.X, tile.Y, tile.Cell);
                    _world.SetTileProperty(winter, tile.X, tile.Y, true, true, false);
                }
                else if (tile.Y < 10)
                {
                    var desert = new Dessert(tile.X, tile.Y, tile.Cell);
                    _world.SetTileProperty(desert, tile.X, tile.Y, true, true, false);
                }
                else
                {
                    var tileType = random.Next(0, 99);
                    if (tileType < 30)
                    {
                        var plains = new Plains(tile.X, tile.Y, tile.Cell);
                        _world.SetTileProperty(plains,tile.X,tile.Y,true,true,false);
                    }else if (tileType < 50)
                    {
                        var Forest = new Plains(tile.X, tile.Y, tile.Cell);
                        _world.SetTileProperty(Forest, tile.X, tile.Y, true, true, false);
                    }
                    else if (tileType < 65)
                    {
                        var hill = new Hills(tile.X, tile.Y, tile.Cell);
                        _world.SetTileProperty(hill, tile.X, tile.Y, true, true, false);
                    }
                    else if (tileType < 75)
                    {
                        var mountain = new Mountain(tile.X, tile.Y, tile.Cell);
                        _world.SetTileProperty(mountain, tile.X, tile.Y, true, true, false);
                    }
                    else if (tileType < 90)
                    {
                        var marsh = new Marsh(tile.X, tile.Y, tile.Cell);
                        _world.SetTileProperty(marsh, tile.X, tile.Y, true, true, false);
                    }
                    else
                    {
                        var plain = new Plains(tile.X,tile.Y,tile.Cell);
                        _world.SetTileProperty(plain, tile.X, tile.Y, true, true, false);
                    }
                }
            }

            var beachTiles = 
                _world.GetTilesAlongLine(island.Left - distance, island.Top - distance, island.Right, island.Top- distance).ToList();
            beachTiles.
                AddRange(_world.GetTilesAlongLine(island.Left - distance, island.Bottom, island.Right, island.Bottom));
            beachTiles.
                AddRange(_world.GetTilesAlongLine(island.Left - distance, island.Top- distance, island.Left- distance, island.Bottom));
            beachTiles.
                AddRange(_world.GetTilesAlongLine(island.Right, island.Top- distance, island.Right, island.Bottom));
            foreach (var t in beachTiles)
            {
                var coast=  new Coast(t.X,t.Y,t.Cell);
                _world.SetTileProperty(coast,t.X,t.Y,true,true,false);
            }
        }

        private void GenerateCircleIsland()
        {

        }
        

        public World GenerateWorldNew(int islandChance = 30, int circleChance = 40, int RectChance = 70, bool LargerContinents = true)
        {
            _world.Initialize(_width,_height);
            var rand=  new Random();

            //And in the begining, god created the sea.
            CreateWorldOcean();
            //Now some Islands!
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    GenerateIslands(islandChance, circleChance, RectChance, LargerContinents, rand);
                }
            }

            return _world;
        }

        //Credit to FaronBracy on github: https://github.com/FaronBracy/RogueSharpRLNetSamples/blob/master/RogueSharpRLNetSamples/Systems/MapGenerator.cs
        // A great tutorial for roguesharp and RLnet IMO, and a huge insperation for the structure of this project. As well as a helpful codebase when needed.
        //All credit to him for inspiring a huge chunk of this code. 
        private void GenerateIslands(int islandChance, 
            int circleChance, int RectChance, bool LargerContinents, Random rand)
        {
            //100 options.
            var result = rand.Next(0, 100);
            //70% chance of an island to spawn
            if (result > islandChance)
            {
                if (result > RectChance)
                {
                    var geneateIsland = GenerateIslandLocation(LargerContinents, rand);
                    if (geneateIsland.HasValue)
                    {
                        var island = geneateIsland.Value;
                        GenerateRectangleIsland(island);
                    }
                }

                //TODO write circile island code. I'm lazy
            }
        }

        private Rectangle?  GenerateIslandLocation(bool LargerContinents, Random rand)
        {
            var islandWidth = 0;
            var islandHeight = 0;
            var islandXPosition = 0;
            var islandYPosition = 0;
            if (LargerContinents)
            {
                islandWidth = rand.Next(ISLAND_MIN_SIZE_LARGE_CONTINENT, ISLAND_MIN_SIZE_LARGE_CONTINENT);
                islandHeight = rand.Next(ISLAND_MIN_SIZE_LARGE_CONTINENT, ISLAND_MAX_SIZE_LARGE_CONTINENT);
                islandXPosition = rand.Next(0, _width);
                islandYPosition = rand.Next(0, _height);

                var newIsland = new Rectangle(islandXPosition, islandYPosition, islandWidth, islandHeight);
                bool islandIntersects = _world.Islands.Any(island => newIsland.Intersects(island));
                if (!islandIntersects)
                {
                    _world.Islands.Add(newIsland);
                    return newIsland;
                }
            }

            return null;
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

            GenerateOceans();

            return _world;
        }
    }
}
