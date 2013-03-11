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
        public float targetMaxDistance;
        public Collector collector;

        private Vector3 origLocation;
        private float speed = 0.05f;
        private Model model;
        private Schedualizer respawnDelay;

        public Resource(Vector3 location)
        {
            model = ContentManager.Model("chicken");
            respawnDelay = new Schedualizer(0, 3, 5);
            this.origLocation = location;
            Initialize();
        }

        public void Initialize()
        {
            location = origLocation;
            collector = null;
        }

        public void Update()
        {
            if (active)
            {
                if (scale < 0.05f) scale += 0.0001f;
                else
                {
                    if (collector != null)
                    {
                        Vector3 offsetFromTarget;

                        Console.WriteLine(CollectorDistance());

                        if (CollectorDistance() < 20f)
                        {
                            offsetFromTarget = GetIntensity(collector.Location(), location);
                        }
                        else
                        {
                            offsetFromTarget = GetIntensity(origLocation, location);
                        }

                        location += offsetFromTarget * speed;
                    }
                }
            }
            if (respawnDelay.IsOn())
            {
                active = true;
            }
        }

        public Vector3 NonDepthLocation()
        {
            return new Vector3(location.X, location.Y, 0);
        }

        public void Gather()
        {
            Initialize();
            active = false;
            scale = 0f;
        }

        public void SetCollector(Collector collector)
        {
            this.collector = collector;
            targetMaxDistance = collector.SuckRadius();
        }

        private float CollectorDistance()
        {
            return Vector3.Distance(location, collector.Location());
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
                ScreenManager.SetTarget(display);
                Camera camera = ScreenManager.Camera(display);
                Drawer3D.Draw(model, location, scale, 1f, camera);
            }
        }

    }
}
