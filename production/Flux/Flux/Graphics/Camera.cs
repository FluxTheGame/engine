using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {

        public Matrix view, projection;
        public Vector3 pos, target, up;
        public int display;

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up) : base(game)
        {
            this.pos = pos;
            this.target = target;
            this.up = up;

            view = Matrix.CreateLookAt(pos, target, up);
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 
                (float)ScreenManager.window.X / (float)ScreenManager.window.Y,
                1, 100);

            Console.WriteLine("ScreenManager Window (via Camera): " + ScreenManager.window.X + " x " + ScreenManager.window.Y);
        }

    }
}
