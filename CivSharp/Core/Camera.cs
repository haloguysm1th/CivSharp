using OpenTK.Input;
using RLNET;

namespace CivSharp.Core
{
    class Camera
    {

        public int X { get; set; }
        public int Y { get; set; }

        public int XSize { get; }
        public int YSize { get; }

        private World _world;

        public Camera(World world, int x, int y, int xSize, int ySize)
        {
            X = x;
            Y = y;
            XSize = xSize;
            YSize = ySize;
            _world = world;
        }

        private (int x, int y) GetMove(RLKey key) =>
            key switch
            {
                RLKey.W  when Y > 0 => (X, Y--),
                RLKey.S  when Y + YSize < _world.Height => (X, Y++),
                RLKey.A when X > 0 => (X--, Y),
                RLKey.D when X + XSize < _world.Width => (X++, Y),
                _ => (X,Y)
            };
        
        public (int X, int Y) Move(RLKeyboard keyboard)
        {
            var keys = keyboard.GetKeyPress();

            if (keys != null)
            {
                return GetMove(keys.Key);
            }

            return (X, Y);
        }
    }
}
