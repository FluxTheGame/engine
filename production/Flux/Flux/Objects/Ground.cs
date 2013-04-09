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
        public Model ground, foliage, water;
        Vector3 position;
        Vector3 rotation;
        VertexPositionColor[] skyboxVerts;
        static BasicEffect skyboxEffect = new BasicEffect(ScreenManager.graphics);
        static BasicEffect waterEffect = new BasicEffect(ScreenManager.graphics);

        public Ground()
        {
            ground = ContentManager.Model(@"env/Ground");
            foliage = ContentManager.Model(@"env/Planes");
            water = ContentManager.Model(@"env/Water");
            position = Vector3.Zero;
            rotation = new Vector3(0, 180, 0);

            skyboxVerts = new VertexPositionColor[4];

            skyboxVerts[0] = new VertexPositionColor(new Vector3(-40, -1, -40), Light.SkyGradientBottom);
            skyboxVerts[1] = new VertexPositionColor(new Vector3(-40, 21, -40),  Light.SkyGradientTop);
            skyboxVerts[2] = new VertexPositionColor(new Vector3(220, -1, -40), Light.SkyGradientBottom);
            skyboxVerts[3] = new VertexPositionColor(new Vector3(220, 21, -40),  Light.SkyGradientTop);
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

                skyboxEffect.World = Matrix.Identity;
                skyboxEffect.View = c.view;
                skyboxEffect.Projection = c.projection;
                skyboxEffect.VertexColorEnabled = true;

                foreach (EffectPass pass in skyboxEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    ScreenManager.graphics.DrawUserPrimitives(PrimitiveType.TriangleStrip, skyboxVerts, 0, 2);
                }

                waterEffect.AmbientLightColor = Color.SteelBlue.ToVector3();

                Drawer3D.Draw(ground, Location(), rotation, c);
                Drawer3D.Draw(water, Location(), rotation, new Vector3(1), 0.4f, c, waterEffect);

                ScreenManager.graphics.DepthStencilState = DepthStencilState.None;
                ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
                Drawer3D.Draw(foliage, Location(), Vector3.Zero, c);
                ScreenManager.graphics.DepthStencilState = DepthStencilState.Default;
            }

        }
    }
}
