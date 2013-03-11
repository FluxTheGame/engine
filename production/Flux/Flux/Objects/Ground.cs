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
        public Model model;
        Vector3 position;
        Vector3 rotation;

        public Ground()
        {
            model = ContentManager.Model(@"env/Ground_Plane");
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
                Drawer3D.Draw(model, Location(), rotation, c);
            }
        }
    }
}
