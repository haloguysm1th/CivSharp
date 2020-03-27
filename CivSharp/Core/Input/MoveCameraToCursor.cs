using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Systems;

namespace CivSharp.Core.Input
{
    class MoveCameraToCursor : InputSystem
    {
        private Cursor _cursor;
        private Camera _camera;
        private World _world;
        public MoveCameraToCursor(InputHandler handler, Camera cam, Cursor cur, World world) : base(handler)
        {
            _cursor = cur;
            _camera = cam;
            _world = world;
        }

        private (int X, int Y) CalculateMove()
        {
            var x = _cursor.X - _cursor.XSize/2;
            var y = _cursor.Y - _cursor.YSize/2;

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            if (x + _cursor.XSize > _world.Width) x = _world.Width - _cursor.XSize;
            if (y + _cursor.YSize > _world.Height) y = _world.Height - _cursor.YSize;



            return (x, y);
        }

        protected override void OnInputEvent(object sender, InputEvent e)
        {
            if (e.Command == InputComands.MoveToCursor)
            {
                var move = CalculateMove();
                _camera.MoveTo(move.X,move.Y);
            }
        }
    }
}
