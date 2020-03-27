using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Interfaces;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Core
{
#nullable enable
    class Resource
    {

    }

    class Tile : IDrawable
    {
        public ICell Cell { get; }
        public RLColor ForegroundColor { get; set; }
        public RLColor BackgroundColor { get; set; }
        public GraphicsItem Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Walkable { get; set; }
        /// <summary>
        /// If the fog of war can see through this
        /// </summary>
        public bool Transparent { get; set; }
        public int Food { get; set; }
        public int Production { get; set; }
        public int MovementRequired { get; set; }
        public Resource? Resource { get; set; }


        //If the tiles was updated, render the update to it, otherwise we don't need to render it.
        public bool WasUpdated { get; set; }

        public IUnit? CurrentUnit;

        public Tile(int x, int y,
            bool walkable, bool transparent, int food, int production, int movementRequired,Resource? resource,
            GraphicsItem symbol, RLColor fg, RLColor bg, ICell cell)
        {
            X = x;
            Y = y;

            Walkable = walkable;
            Transparent = transparent;
            Food = food;
            Production = production;
            MovementRequired = movementRequired;
            Resource = resource;

            ForegroundColor = fg;
            BackgroundColor = bg;
            Symbol = symbol;
            Cell = cell;
        }

        public Tile(Tile oldTile, GraphicsItem symbol, RLColor fg, RLColor bg,
            bool? walkable = null, bool? transparent = null, int? food = null,
            int? production = null, int? movementRequired = null, Resource? resource = null)
        {
            X = oldTile.X;
            Y = oldTile.Y;

            Walkable = walkable ?? oldTile.Walkable;
            Transparent = transparent ?? oldTile.Transparent;
            Food = food ?? oldTile.Food;
            Production = production ?? oldTile.Production;
            MovementRequired = movementRequired ?? oldTile.MovementRequired;
            Resource =resource ?? oldTile.Resource;

            Symbol = symbol;
            ForegroundColor = fg;
            BackgroundColor = bg;
            WasUpdated = true;
        }

        public void Draw(RLConsole console, IMap map)
        {
                console.Set(Cell.X, Cell.Y, ForegroundColor, BackgroundColor, (int) Symbol);
                WasUpdated = false;
        }
    }
}
