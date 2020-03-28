using System;
using RLNET;

namespace CivSharp.Systems
{
    class InputEvent : EventArgs
    {

        public InputComands Command { get; }
        public RLKey Key { get; }

        public InputEvent(InputComands command, RLKey key)
        {
            Command = command;
            Key = key;
        }
    }
}