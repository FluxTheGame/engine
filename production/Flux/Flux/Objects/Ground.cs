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
        VertexPositionColor[] skyboxVerts;
        static BasicEffect Effect = new BasicEffect(ScreenManager.graphics);

        public Ground()
        {
            ground = ContentManager.Model(@"env/Ground");
            foliage = ContentManager.Model(@"env/Planes");
            position = Vector3.Zero;
            rotation = new Vector3(0, 180, 0);

            skyboxVerts = new VertexPositionColor[4];

            skyboxVerts[0] = new VertexPositionColor(new Vector3(-30, -8, -30), Light.SkyGradientBottom);
            skyboxVerts[1] = new VertexPositionColor(new Vector3(-30, 17, -30),  Light.SkyGradientTop);
            skyboxVerts[2] = new VertexPositionColor(new Vector3(300, -8, -30), Light.SkyGradientBottom);
            skyboxVerts[3] = new VertexPositionColor(new Vector3(300, 17, -30),  Light.SkyGradientTop);
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

                Effect.World = Matrix.Identity;
                Effect.View = c.view;
                Effect.Projection = c.projection;
                Effect.VertexColorEnabled = true;

                foreach (EffectPass pass in Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    ScreenManager.graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, skyboxVerts, 0, 2);
                }

                Drawer3D.Draw(ground, Location(), rotation, c);

                ScreenManager.graphics.DepthStencilState = DepthStencilState.None;
                ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
                Drawer3D.Draw(foliage, Location(), Vector3.Zero, c);
                ScreenManager.graphics.DepthStencilState = DepthStencilState.Default;
            }

        }
    }
}
