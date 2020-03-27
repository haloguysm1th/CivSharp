using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Systems;

namespace CivSharp.Core
{
    class Cursor
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int XSize { get; }
        public int YSize { get; }

        private World _world;
        private bool firstTime = true;
        public Cursor(World world, InputHandler inputHandler, int x, int y, int xSize, int ySize)
        {
            X = x;
            Y = y;
            XSize = xSize;
            YSize = ySize;
            _world = world;
            inputHandler.HandleInputEvent += OnKeyPress;

            var tile = world.Tiles[y, x];
            var bg = tile.BackgroundColor;
            var fg = tile.ForegroundColor;
            tile.BackgroundColor = fg;
            tile.ForegroundColor = bg;
        }

        private (int x, int y) GetMove(InputComands command) =>
            command switch
            {
                InputComands.CursorUp when Y > 0 => (X, Y--),
                InputComands.CursorDown when Y + YSize < _world.Height => (X, Y++),
                InputComands.CursorLeft when X > 0 => (X--, Y),
                InputComands.CursorRight when X + XSize < _world.Width => (X++, Y),
                _ => (X, Y)
            };

        private void OnKeyPress(object sender, InputEvent e)
        {

            var previousTile = _world.Tiles[Y, X];
            var foreground = previousTile.ForegroundColor;
            var background = previousTile.BackgroundColor;
            previousTile.ForegroundColor = background;
            previousTile.BackgroundColor = foreground;

            var movementChange = GetMove(e.Command);


            var currentTile = _world.Tiles[Y, X];
            foreground = currentTile.ForegroundColor;
            background = currentTile.BackgroundColor;
            currentTile.ForegroundColor = background;
            currentTile.BackgroundColor = foreground;
        }
    }
}
