using CivSharp.Interfaces;
using CivSharp.Systems;
using RLNET;
using RogueSharp;

namespace CivSharp.Core
{
    class Worker : IUnit
    {
        public Tile CurrentTile { get; private set; }
        public int Speed { get; }
        public int health { get; }
        public int Strength { get; }

        public void MoveTile(Tile targetTile)
        {
            CurrentTile = targetTile;
        }

        public void UseAbility(IAbility ability)
        {
            throw new System.NotImplementedException();
        }

        public RLColor Color { get; set; }
        public RLColor ForegroundColor { get; set; }
        public RLColor BackgroundColor { get; set; }
        public GraphicsItem Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            throw new System.NotImplementedException();
        }
    }
}