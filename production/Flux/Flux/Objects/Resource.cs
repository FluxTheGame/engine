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
        public bool active = true;
        public int display = 0;
        public float scale = 0.05f;
        public Vector3 target;

        private Vector3 origLocation;
        private float speed = 0.01f;
        private Model model;
        private Schedualizer respawnDelay;

        public Resource(Vector3 location)
        {
            model = ContentManager.Model("chicken");

            respawnDelay = new Schedualizer(0, 3, 5);

            this.location = location;
            this.target = location;
            this.origLocation = location;
        }

        public void Update()
        {
            if (active)
            {
                if (scale < 0.05f) scale += 0.0001f;
                else
                {
                    Vector3 offsetFromTarget = GetIntensity(target, location);
                    location += offsetFromTarget * speed;
                }
            }
        }

        public Vector3 NonDepthLocation()
        {
            return new Vector3(location.X, location.Y, 0);
        }

        public void Gather()
        {
            location = origLocation;
            //active = false;
            scale = 0f;
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
            if (active)
            {
                Camera camera = ScreenManager.Camera(display);
                Drawer3D.Draw(model, location, scale, 1f, camera);
            }
        }

    }
}
