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
        private static Vector2 window;


        public static void Initialize(Game game, GraphicsDevice g)
        {
            camera[0] = new Camera(game,new Vector3(0, 0, 8), Vector3.Zero, Vector3.Up);
            camera[0].display = 0;

            graphics = g;
            window = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
        }

        public static Camera Camera(int display)
        {
            return camera[display];
        }

        public static GraphicsDevice Graphics(int display)
        {
            return graphics;
        }

        public static Vector3 Location(Vector2 position)
        {
            Vector2 m = Middle();
            return new Vector3((position.X - m.X) * 0.01f, (position.Y - m.Y) * -0.01f, 0.0f);
        }

        public static Vector2 Middle()
        {
            return Vector2.Multiply(window, 0.5f);
        }

    }
}
