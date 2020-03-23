using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Interfaces
{
    interface IDrawable
    {
        RLColor ForegroundColor { get; set; }
        RLColor BackgroundColor { get; set; }
        GraphicsItem Symbol { get; set; }
        int X { get; set; }
        int Y { get; set; }
        void Draw(RLConsole console, IMap map);
    }
}
