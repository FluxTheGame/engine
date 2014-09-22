using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    class Cloud
    {
        Vector3 location;
        float velocity = Randomizer.RandomFloat(0.002f, 0.007f);
        Model model;

        public Cloud(Model m, Vector3 loc)
        {
            model = m;
            location = loc;
            //location = Vector3.Zero;
        }

        public void Update()
        {
            location.X += velocity;

            int display = ScreenManager.DisplayOfLocation(location);

            if (display == 3)
            {
                Camera c = ScreenManager.Camera(display);
                Vector3 screenPos = ScreenManager.tmpViewport.Project(location, c.projection, c.view, Matrix.Identity);

                if (screenPos.X > 1280)
                {
                    location.X = -50;
                }
            }
        }

        public void Draw()
        {
            // draw on every camera
            for (int i = 0; i < 4; i++)
            {
                ScreenManager.SetTarget(i);
                Camera c = ScreenManager.Camera(i);
                Drawer3D.Draw(model, location, new Vector3(0,90,0), new Vector3(0.1f), 0.6f, c, CloudManager.cloudLighting);
            }
        }
    }
}
