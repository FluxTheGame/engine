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

        public Cloud(Model m, int id)
        {
            model = m;
            location = new Vector3(Randomizer.RandomFloat(-6, 200), Randomizer.RandomFloat(1, 5), Randomizer.RandomFloat(-20, -35)/*(id*-2)-1*/);
            //location = Vector3.Zero;
        }

        public void Update()
        {
            location.X += velocity;

            if (location.X > 200)
                location.X = -7f;

            // The most random calculations in the entire codebase...probably
            int div = (int)Math.Round((location.X + 6) / 60);

            Camera c = ScreenManager.Camera(div);
            Vector3 screenPos = ScreenManager.tmpViewport.Project(location, c.projection, c.view, Matrix.Identity);

            float normalized = screenPos.X - 60 * div;

            if (normalized > 7 && normalized < 53)
            {
                screenPos.X += 46;
                location.X = ScreenManager.tmpViewport.Project(screenPos, c.projection, c.view, Matrix.Identity).X;
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
