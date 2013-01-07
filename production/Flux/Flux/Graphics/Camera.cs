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

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up) : base(game)
        {
            view = Matrix.CreateLookAt(pos, target, up);
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)Game.Window.ClientBounds.Width /
                (float)Game.Window.ClientBounds.Height,
                1, 100);
        }

       
        public override void Initialize()
        {
            base.Initialize();
        }

       
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
