using System;
using CivSharp.Core.Input;
using CivSharp.Systems;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RLNET;

namespace CivSharp.Core
{
    class Camera : InputSystem
    {

        public int X { get; set; }
        public int Y { get; set; }

        public int XSize { get; }
        public int YSize { get; }

        private World _world;

        public Camera(World world, InputHandler inputHandler, int x, int y, int xSize, int ySize) : base(inputHandler)
        {
            X = x;
            Y = y;
            XSize = xSize;
            YSize = ySize;
            _world = world;
        }
        public void MoveTo(int x, int y)
        {
            X = x;
            Y = y;
            Game.CameraX = X;
            Game.CameraY = Y;
        }

        private (int x, int y) GetMove(InputComands command) =>
            command switch
            {
                InputComands.CameraUp when Y > 0 => (X, Y--),
                InputComands.CameraDown when Y + YSize < _world.Height => (X, Y++),
                InputComands.CameraLeft when X > 0 => (X--, Y),
                InputComands.CameraRight when X + XSize < _world.Width => (X++, Y),
                _ => (X, Y)
            };

        protected override void OnInputEvent(object sender, InputEvent e)
        {
            GetMove(e.Command);
            Game.CameraX = X;
            Game.CameraY = Y;
        }
    }
}
