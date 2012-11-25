using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Flocking_002
{

    public class Camera : Microsoft.Xna.Framework.GameComponent
    {

        //camera marticies
        public Matrix view;
        public Matrix projection;
        public BoundingFrustum frustum;

        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up) : base(game)
        {
            view = Matrix.CreateLookAt(pos, target, up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), game.GraphicsDevice.Viewport.AspectRatio, 1.0f, 10000.0f);
            frustum = new BoundingFrustum(view * projection);
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
