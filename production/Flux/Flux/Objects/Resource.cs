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


namespace Flux
{
    public class Resource
    {
        public Vector3 location;
        private Vector3 origLocation;
        int display = 0;

        Model model;
        float scale = 10.0f;
        public Vector3 target;
        private float speed = 0.01f;

        public Resource(Vector3 location)
        {
            model = ContentManager.Model("chicken");
            scale = 0.01f;

            this.location = location;

            target = location;
            origLocation = location;
        }

        public void Update()
        {
            Vector3 offsetFromTarget = GetIntensity(target, location);
            location += offsetFromTarget * speed;
        }

        public Vector3 NonDepthLocation()
        {
            return new Vector3(location.X, location.Y, 0);
        }

        private Vector3 GetIntensity(Vector3 aim, Vector3 loc)
        {
            float dist = Vector3.Distance(aim, NonDepthLocation());

            float intensity = 0f;
            if (dist > 0) intensity = 1.0f / (dist * dist); // inverse square

            Vector3 offset = (aim - loc) * intensity;

            return offset;
        }

        public void Draw()
        {
            Camera camera = ScreenManager.Camera(display);
            Drawer3D.Draw(model, location, scale, camera);
        }

    }
}
