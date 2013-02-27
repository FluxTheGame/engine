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
        private static int spacing = 2000;

        public static SpriteBatch spriteBatch;
        public static Vector2 window; //Size of a single window
        public static Vector2 world; //Size of entire world (4 windows)
        public static int screens = 4;


        public static void Initialize(Game game, GraphicsDevice g)
        {
            camera[0] = new Camera(game,new Vector3(0.16f, -0.05f, 9f), new Vector3(0.16f, -0.05f, 0), Vector3.Up);
            camera[0].display = 0;

            graphics = g;
            spriteBatch = new SpriteBatch(graphics);
            window = new Vector2(game.Window.ClientBounds.Width, game.Window.ClientBounds.Height);
            world = new Vector2(window.X * screens, window.Y);
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
            Vector2 mid = Middle();
            Vector3 pos = new Vector3((position.X - mid.X + (spacing*display)) * 0.00952f, (position.Y - mid.Y) * -0.0099f, 0.0f);
            return pos;
        }

        public static Vector2 Middle()
        {
            return new Vector2(window.X * 0.5f, window.Y * 0.5f);
        }

        public static int Opposite(int display)
        {
            return (display + 2) % 4;
        }

    }
}
