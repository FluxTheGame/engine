using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    class ScreenManager
    {
        private static Camera[] camera = new Camera[1]; //Array of cameras
        private static GraphicsDevice graphics;
        public static Vector2 window;


        public static void Initialize(Game game, GraphicsDevice g)
        {
            camera[0] = new Camera(game,new Vector3(0, 0, 10), new Vector3(0, 0, 0), Vector3.Up);
            camera[0].display = 0;

            graphics = g;
            window = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
        }

        public static Camera Camera(int display)
        {
            return camera[0];
        }

        public static GraphicsDevice Graphics(int display)
        {
            return graphics;
        }

        public static Vector3 Location(Vector2 position, int display)
        {
            float displayOffset = ((window.X+100) * display);
            Vector2 mid = Middle();
            Vector3 pos = new Vector3((position.X + displayOffset - mid.X) * 0.01f, (position.Y - mid.Y) * -0.01f, 0.0f);
            return pos;
        }

        public static Vector2 Middle()
        {
            return Vector2.Multiply(window, 0.5f);
        }

    }
}
