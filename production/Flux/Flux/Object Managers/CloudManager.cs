using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    class CloudManager
    {
        public static Model[] models = new Model[6];
        public static Cloud[] clouds = new Cloud[6];

        public static BasicEffect cloudLighting = new BasicEffect(ScreenManager.graphics);

        public static void LoadContent()
        {
            cloudLighting.AmbientLightColor = Color.White.ToVector3();

            for (int i = 0; i < 6; i++) {

                models[i] = ContentManager.Model(@"env/Clouds_0" + (i+1));
                //models[i] = ContentManager.Model(@"chicken");
                clouds[i] = new Cloud(models[i], i);
            }
        }

        public static void Update()
        {
            for (int i = 0; i < 6; i++)
            {
                clouds[i].Update();
            }
        }

        public static void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 6; i++)
            {
                clouds[i].Draw();
            }
        }
    }
}
