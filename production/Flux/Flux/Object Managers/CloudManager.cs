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
        public static Model[] models = new Model[5];
        public static Cloud[] clouds = new Cloud[8];

        public static BasicEffect cloudLighting = new BasicEffect(ScreenManager.graphics);

        public static void LoadContent()
        {
            cloudLighting.AmbientLightColor = Color.White.ToVector3();

            models[0] = ContentManager.Model(@"env/Clouds_1");
            models[1] = ContentManager.Model(@"env/Clouds_2");
            models[2] = ContentManager.Model(@"env/Clouds_3");
            models[3] = ContentManager.Model(@"env/Clouds_4");
            models[4] = ContentManager.Model(@"env/Clouds_5");

            clouds[0] = new Cloud(models[0], new Vector3(-20, /*8:15*/8, /*-20:-35*/-30));
            clouds[1] = new Cloud(models[1], new Vector3(50, /*8:15*/11, /*-20:-35*/-35));
            clouds[2] = new Cloud(models[2], new Vector3(100, /*8:15*/9, /*-20:-35*/-25));
            clouds[3] = new Cloud(models[3], new Vector3(160, /*8:15*/10, /*-20:-35*/-25));
            clouds[4] = new Cloud(models[4], new Vector3(135, /*8:15*/11, /*-20:-35*/-35));
            clouds[5] = new Cloud(models[0], new Vector3(10, /*8:15*/12, /*-20:-35*/-32));
            clouds[6] = new Cloud(models[2], new Vector3(190, /*8:15*/9, /*-20:-35*/-28));
        }

        public static void Update()
        {
            for (int i = 0; i < 7; i++)
            {
                clouds[i].Update();
            }
        }

        public static void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 7; i++)
            {
                clouds[i].Draw();
            }
        }
    }
}
