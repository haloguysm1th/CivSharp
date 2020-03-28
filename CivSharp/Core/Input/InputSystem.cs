using CivSharp.Systems;

namespace CivSharp.Core.Input
{
    abstract class InputSystem
    {

        private InputHandler _handler;
        public InputSystem(InputHandler handler)
        {
            _handler = handler;
            _handler.HandleInputEvent += OnInputEvent;
        }

        protected abstract void OnInputEvent(object sender, InputEvent e);
    }
}