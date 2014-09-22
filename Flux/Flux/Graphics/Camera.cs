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
        }

       
        public override void Initialize()
        {
            base.Initialize();
        }

       
        public override void Update(GameTime gameTime)
        {

            /* Move camera for debugging */
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Left))
            {
                pos.X += 0.1f;
            }
            if (keyState.IsKeyDown(Keys.Right))
            {
                pos.X -= 0.1f;
            }

            view = Matrix.CreateLookAt(pos, target, up);
            /* End */

            base.Update(gameTime);
        }

    }
}
