using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    public class Ground
    {
        public Model ground, foliage;
        Vector3 position;
        Vector3 rotation;

        public Ground()
        {
            ground = ContentManager.Model(@"env/Ground");
            foliage = ContentManager.Model(@"env/Planes");
            position = Vector3.Zero;
            rotation = new Vector3(0, 180, 0);
        }

        public Vector3 Location()
        {
            return position;
        }

        public void Draw()
        {
            for (int i = 0; i < ScreenManager.screens; i++)
            {
                ScreenManager.SetTarget(i);
                Camera c = ScreenManager.Camera(i);
                Drawer3D.Draw(ground, Location(), rotation, c);

                ScreenManager.graphics.DepthStencilState = DepthStencilState.None;
                ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
                Drawer3D.Draw(foliage, Location(), Vector3.Zero, c);
                ScreenManager.graphics.DepthStencilState = DepthStencilState.Default;
            }
        }
    }
}
