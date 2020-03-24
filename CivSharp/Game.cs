using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CivSharp.Core;
using CivSharp.Systems;
using RLNET;

namespace CivSharp
{

    class Game
    {
        #region Readonly Variables

        private static readonly int _screenWidth = 100;
        private static readonly int _screenHeight = 70;

        
        private static readonly int _unitWidth = 20;
        private static readonly int _unitHeight = _screenHeight;
        
        private static readonly int _commandButtonHeight = 10;
        private static readonly int _commandButtonWidth = _screenWidth - _unitWidth;

        private static readonly int _commandTextWidth = _commandButtonWidth;
        private static readonly int _commandTextHeight = 10;

        private static readonly int _commandHeight = _commandButtonHeight + _commandHeight;
        private static readonly int _commandWidth = _commandButtonWidth;

        //The render size for the map view
        private static readonly int _mapRenderWidth = _screenWidth - _unitWidth;
        private static readonly int _mapRenderHeight = _screenHeight - _commandHeight;

        //The real size of the map
        private static readonly int _mapWidth = 200;
        private static readonly int _mapHeight = 140;

        #endregion
        #region Consoles
        private static RLConsole _mapConsole;
        private static RLConsole _commandConsole;
        private static RLConsole _unitConsole;
        private static RLRootConsole _rootConsole;
        #endregion
        #region Local variables
        private static World world;
        private static Camera _camera;

        private static bool fullScreen = false;
        private static int _cameraX = 0;
        private static int _cameraY= 0;
        #endregion
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args[0] == "full") fullScreen = true;
            }

            var font = "civGraphics.png";
            var consoleTitle = "CivSharp";
            var charHeight = 8;
            var charWidth = 8;
            var scale = 1.6f;
            var settings = new RLSettings();
            settings.Width = _screenWidth;
            settings.Height = _screenHeight;
            settings.BitmapFile = font;
            settings.Title = consoleTitle;
            settings.CharHeight = charHeight;
            settings.CharWidth = charWidth;
            settings.Scale = scale;
            settings.WindowBorder = RLWindowBorder.Fixed;
            settings.StartWindowState = fullScreen ? RLWindowState.Fullscreen : RLWindowState.Normal;

            _rootConsole = new RLRootConsole(settings);

            _mapConsole = new RLConsole(_mapWidth,_mapHeight);
            _commandConsole = new RLConsole(_commandWidth,_commandHeight);
            _unitConsole = new RLConsole(_unitWidth,_unitHeight);

            var generator=  new WorldGenerator(_mapWidth, _mapHeight);
            world = generator.GenerateWorld();

            //Pass render height/width because we add them as offsets.
            _camera = new Camera(world,0,0,_mapRenderWidth,_mapRenderHeight);

            _rootConsole.Update += OnRootConsoleUpdate;
            _rootConsole.Render += OnRootConsoleRender;
            _rootConsole.Run();
        }
        #region Private Methodws

        private static void MoveCamera()
        {
            var cameraMove = _camera.Move(_rootConsole.Keyboard);
            _cameraX = cameraMove.X;
            _cameraY = cameraMove.Y;
        }

        #endregion

        #region Rendering and Updating
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            _commandConsole.Clear();
            _mapConsole.Clear();
            _unitConsole.Clear();

            MoveCamera();

            _commandConsole.SetBackColor(0,0,_commandWidth,_commandHeight,RLColor.Cyan);
            _commandConsole.Print(1, 1, "Commands!",RLColor.White);

            _unitConsole.SetBackColor(0,0,_unitWidth,_unitHeight,RLColor.Magenta);
            _unitConsole.Print(1, 1, "Units!",RLColor.White);

            world.Update();
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            //Draw the world
            world.Draw(_mapConsole);

            //Put all the consoles into the rootConsole
            RLConsole.Blit(_mapConsole,_cameraX,_cameraY,_mapRenderWidth,_mapRenderHeight,
                _rootConsole,_unitWidth,_commandHeight);
            RLConsole.Blit(_unitConsole,0,0,_unitWidth,_unitHeight,
                _rootConsole,0,0);
            RLConsole.Blit(_commandConsole,0,0,_commandWidth,_commandHeight,
                _rootConsole,_unitWidth,0);

            //Then render the root console (and thus all the other ones)
            _rootConsole.Draw();
        }
        #endregion  
    }
}
