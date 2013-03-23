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
        public Vector2 position;
        public bool active = true;
        public int display = 0;
        public float scale = 0.1f;
        public float targetMaxDistance;
        public Collector collector;

        private Vector3 origLocation;
        private float speed = 0.05f;
        private Model model;
        private Schedualizer respawnDelay;

        public Resource(Vector3 location, string modelName)
        {
            model = ContentManager.Model(modelName);
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

            Vector3 offsetFromTarget = Vector3.Zero;

            Matrix proj = ScreenManager.Camera(display).projection;
            Matrix view = ScreenManager.Camera(display).view;
            Vector3 screenPos = ScreenManager.graphics.Viewport.Project(location, proj, view, Matrix.Identity);
            position = new Vector2(screenPos.X, screenPos.Y);


            if (active)
            {
                // easing
                if (scale < 0.1f) scale += (0.1f - scale) * 0.1f;
                else if (collector != null)
                {
                    if (CollectorDistance() < 100f)
                        offsetFromTarget = GetIntensity(collector.Location(), location);
                    else
                        offsetFromTarget = GetIntensity(origLocation, location);

                    if (collector.isDying)
                        collector = null;
                }
                else
                {
                    offsetFromTarget = GetIntensity(origLocation, location);
                }

                location += offsetFromTarget * speed;

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
            //return Vector3.Distance(location, collector.Location());

            return Vector2.Distance(position, collector.position);
        }

        private Vector3 GetIntensity(Vector3 aim, Vector3 loc)
        {
            float dist = Vector3.Distance(aim, NonDepthLocation());

            /*float intensity = 0f;
            if (dist > 0) intensity = 1.0f / (dist * dist); // inverse square*/

            Vector3 offset = (aim - loc);

            return offset;
        }

        public void Draw()
        {
            if (active)
            {
                ScreenManager.SetTarget(display);
                Camera camera = ScreenManager.Camera(display);
                ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
                Drawer3D.Draw(model, location, scale, 1f, camera);
            }
        }

    }
}
