using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace SpriteEditor
{
    class Program
    {
        private static RLRootConsole console;
        static void Main(string[] args)
        {
            var graphicsName = "civGraphics.png";
            var settings = new RLSettings();
            settings.BitmapFile = graphicsName;
            settings.Width = 50;
            settings.Height = 35;
            settings.CharHeight = 8;
            settings.CharWidth = 8;
            settings.Title = $"Sprite Editor | working on: {graphicsName}";
            settings.Scale = 1.2f;
            
            console= new RLRootConsole(settings);

            console.Update += Update;
            console.Render += Render;
            console.Run();
        }

        static void Update(object sender, UpdateEventArgs e)
        {

        }

        static void Render(object sender, UpdateEventArgs e)
        {

        }
    }
}
