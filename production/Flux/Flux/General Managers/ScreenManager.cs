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


        public static void Initialize(Game game, GraphicsDevice g)
        {
            camera[0] = new Camera(game,new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up);
            camera[0].display = 0;

            graphics = g;
        }

        public static Camera Camera(int display)
        {
            return camera[display];
        }

        public static GraphicsDevice Graphics(int display)
        {
            return graphics;
        }

    }
}
