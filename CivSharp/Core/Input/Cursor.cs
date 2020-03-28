using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core.Input;
using CivSharp.Systems;
using RLNET;

namespace CivSharp.Core
{
    enum GUIParts
    {
        StraightTop = 126,
        TopLeft = 127,
        TopRight = 128,
        DownLeft = 129,
        DownRight = 130,
        BottomLeft = 131,
        BottomRight = 132,
        Bottom = 133
    }
    class Cursor : InputSystem
    {
        #region Variables
        public int X { get; set; }
        public int Y { get; set; }

        public int XSize { get; }
        public int YSize { get; }

        private World _world;
        private bool firstTime = true;
        private RLConsole _unitConsole;
        #endregion
        #region Constructor
        public Cursor(World world, InputHandler inputHandler,RLConsole unitConsole ,int x, int y, int xSize, int ySize) : base(inputHandler)
        {
            X = x;
            Y = y;
            XSize = xSize;
            YSize = ySize;
            _world = world;

            var tile = world.Tiles[y, x];
            var bg = tile.BackgroundColor;
            var fg = tile.ForegroundColor;
            tile.BackgroundColor = fg;
            tile.ForegroundColor = bg;
        }
        #endregion
        #region Public methods

        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Update(RLConsole unitConsole)
        {
            var tile = _world.Tiles[Y, X];
            var colour = tile.BackgroundColor;
            var y = 1;
            var x = 2;

            unitConsole.SetBackColor(0,0,unitConsole.Width,unitConsole.Height,colour);

            unitConsole.Print(x, y++, $"Type: {tile.Symbol}", RLColor.White);
            unitConsole.Print(x, y++, $"X: {tile.X} | Y: {tile.Y}",RLColor.White);
            unitConsole.Print(x, y++, $"Food: {tile.Food}", RLColor.White);
            unitConsole.Print(x, y++, $"Production: {tile.Production}", RLColor.White);
            unitConsole.Print(x, y++, $"Movement: {tile.MovementRequired}", RLColor.White);
            unitConsole.Print(x, y++, $"Walkable: {tile.Walkable}", RLColor.White);

            var resourceName = $"Resource: None";
            var resourceAmount = $"Amount: 0";

            if (tile.Resource != null)
            {
                resourceName = $"Resource: {tile.Resource.Name}";
                resourceAmount= $"Amount: {tile.Resource.Amount}";
            }

            unitConsole.Print(x, y++, resourceName, RLColor.White);
            unitConsole.Print(x, y++, resourceAmount, RLColor.White);

            DrawBox(unitConsole,y);
        }
        #endregion


        #region Private Methods

        private void TopBar(RLConsole unitConsole)
        {
            var text = ((char)GUIParts.TopLeft).ToString();

            for (int i = 1; i < unitConsole.Width - 1; i++)
            {
                text += ((char)GUIParts.StraightTop).ToString();
            }
            text += (char)GUIParts.TopRight;
            unitConsole.Print(0, 0, text, RLColor.White);
        }

        private void BottomBar(RLConsole unitConsole, int y)
        {
            var text = ((char) GUIParts.BottomLeft).ToString();
            for (int x = 1; x < unitConsole.Width - 1; x++)
            {
                text += ((char) GUIParts.Bottom).ToString();
            }

            text += ((char) GUIParts.BottomRight).ToString();
            unitConsole.Print(0, y, text, RLColor.White);
        }

        private void AddSides(RLConsole console, int y)
        {
            console.Set(0,y,RLColor.White, null,(int) GUIParts.DownLeft);
            console.Set(console.Width - 1, y,RLColor.White, null,(int) GUIParts.DownRight);
        }

        private void DrawBox(RLConsole unitConsole, int height)
        {
            TopBar(unitConsole);
            for (int i = 1; i < height; i++)
            {
                AddSides(unitConsole,i);
            }

            BottomBar(unitConsole,height);
        }

        private (int x, int y) GetMove(InputComands command) =>
            command switch
            {
                InputComands.CursorUp when Y > 0 => (X, Y--),
                InputComands.CursorDown when Y < _world.Height-1 => (X, Y++),
                InputComands.CursorLeft when X > 0 => (X--, Y),
                InputComands.CursorRight when X < _world.Width-1 => (X++, Y),
                _ => (X, Y)
            };
        #endregion

        protected override void OnInputEvent(object sender, InputEvent e)
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
