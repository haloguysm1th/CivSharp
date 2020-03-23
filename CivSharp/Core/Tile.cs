using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Interfaces;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Core
{
#nullable enable
    class Tile : IDrawable
    {
        public ICell Cell { get; }
        public RLColor ForegroundColor { get; set; }
        public RLColor BackgroundColor { get; set; }
        public GraphicsItem Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        //If the tiles was updated, render the update to it, otherwise we don't need to render it.
        public bool WasUpdated { get; set; }

        public IUnit? CurrentUnit;

        public Tile(int x, int y, GraphicsItem symbol, RLColor fg, RLColor bg, ICell cell)
        {
            X = x;
            Y = y;
            ForegroundColor = fg;
            BackgroundColor = bg;
            Symbol = symbol;
            Cell = cell;
        }

        public void Draw(RLConsole console, IMap map)
        {
            console.Set(Cell.X, Cell.Y, ForegroundColor,BackgroundColor, (int)Symbol);
        }
    }
}
