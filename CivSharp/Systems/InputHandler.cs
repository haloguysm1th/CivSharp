using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core;
using Newtonsoft.Json;
using RLNET;

namespace CivSharp.Systems
{
    enum InputComands
    {
        CameraLeft,
        CameraRight,
        CameraUp,
        CameraDown,
        CursorLeft,
        CursorRight,
        CursorUp,
        CursorDown,
        Enter,
        Esc
    }

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

    class InputHandler
    {
        public delegate void InputEventHandler(object sender, InputEvent e);

        public event InputEventHandler HandleInputEvent;

        private Dictionary<RLKey, InputComands> _inputDictionary;
        private RLKeyboard _keyboard;

        //The letters to their rlKey equivilent
        private Dictionary<string, RLKey> _inputChars = new Dictionary<string, RLKey>()
        {
            {"w",RLKey.W},
            { "a",RLKey.A},
            { "d",RLKey.D},
            {"s",RLKey.S},
            {"left",RLKey.Left},
            {"right" ,RLKey.Right},
            {"up", RLKey.Up},
            {"down",RLKey.Down},
            {"enter",RLKey.Enter},
            {"esc",RLKey.Escape}
        };

        private  Camera _camera;
        private Cursor _cursor;

        public InputHandler(RLKeyboard keyboard,World world,
            (int width, int height) viewport,string configName="/settings.json")
        {
            string file = null;
            Dictionary<string, string> keyboardBindings = new Dictionary<string, string>
            {
                { "CameraLeft","a"},
                {"CameraRight","d" },
                {"CameraUp","w" },
                {"CameraDown","s" },
                {"CursorLeft","left" },
                {"CursorRight","right" },
                {"CursorUp","up" },
                {"CursorDown","down" },
                {"Esc","esc" },
                {"Enter","enter" }
            };

            try
            {
                file = File.ReadAllText(Directory.GetCurrentDirectory() + configName);
                keyboardBindings =
                    JsonConvert.DeserializeObject<Dictionary<string, string>>(file);
            }
            catch (FileNotFoundException)
            {
                var filePath = Directory.GetCurrentDirectory() + configName;
                var json = JsonConvert.SerializeObject(keyboardBindings);
                File.WriteAllText(filePath,json);
                Console.WriteLine($"Wrote config file to: {filePath}");
            }

            _inputDictionary = new Dictionary<RLKey, InputComands>();
            _keyboard = keyboard;

            foreach (var binding in keyboardBindings)
            {
                var command = ConvertToInputCommands(binding.Key);
                var key = _inputChars[binding.Value];
                _inputDictionary.Add(key, command);
            }
            
            _camera = new Camera(world, this, 0, 0, viewport.width, viewport.height);
            _cursor = new Cursor(world, this, 0, 0,viewport.width,viewport.height );
        }

        private InputComands ConvertToInputCommands(string s)
        {
            switch (s)
            {
                case "CameraLeft": return InputComands.CameraLeft;
                case "CameraRight": return InputComands.CameraRight;
                case "CameraUp": return InputComands.CameraUp;
                case "CameraDown": return InputComands.CameraDown;
                case "CursorDown": return InputComands.CursorDown;
                case "CursorUp": return InputComands.CursorUp;
                case "CursorLeft": return InputComands.CursorLeft;
                case "CursorRight": return InputComands.CursorRight;
                case "Enter": return InputComands.Enter;
                case "Esc": return InputComands.Esc;
                default: return InputComands.Esc;
            }
        }

        public void Update()
        {
            var keys = _keyboard.GetKeyPress();
            
            if (keys != null)
            {
                var key = keys.Key;
                try
                {
                    var command = _inputDictionary[key];
                    OnRaisedInputEvent(new InputEvent(command, key));
                }
                catch (KeyNotFoundException) { }
            }
        }

        protected virtual void OnRaisedInputEvent(InputEvent e)
        {
            var handler = HandleInputEvent;

            handler?.Invoke(this, e);
        }
    }
}
