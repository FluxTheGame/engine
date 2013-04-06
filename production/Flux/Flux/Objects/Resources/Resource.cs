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
        public Vector2 position;
        public Vector3 location;
        public float scale = 0f;
        public int display = 0;
        public float opacity = 1f;
        public Vector3 rotation;
        public Collector collector;
        public Schedualizer addDelay;

        protected float speed;
        protected const float minScale = 0f;
        protected const float maxScale = 1f;
        protected float modelScale = 0.07f;
        protected Model model;


        public Resource(Vector3 location, string modelName, float speed)
        {
            model = ContentManager.Model(modelName);
            this.speed = speed;
            this.location = location;
            addDelay = new Schedualizer(0, 1, 30);
            rotation = Vector3.Zero;
        }

        public virtual void Activate()
        {
            // Ease interpolates minScale->maxScale, over 500 "time units"
            Tweenerizer.Ease(EasingType.EaseIn, minScale, maxScale, 500, (ease, incr) =>
            {
                scale = ease;
            });
        }

        public virtual void AssignCollector(Collector collector)
        {
            this.collector = collector;
            //Override this method to get a call whenever a collector is assigned to this resource
        }

        public virtual void Update()
        {
            position = ScreenManager.ProjectedPosition(location, display);

            if (collector != null)
            {
                if (Vector2.Distance(ScreenManager.AdjustedPosition(position, display), ScreenManager.AdjustedPosition(collector.position, collector.display)) < 30)
                {
                    BeCollected();
                }
                else
                {
                    MoveToCollector();
                }
            }
        }

        protected virtual void MoveToCollector()
        {
            //Override this method to implement resource movement logic
            if (!collector.isDying)
            {
                Vector3 collectorLocation = collector.Location();
                Vector3 offset = ((collectorLocation + new Vector3(0, 0, -0.25f)) - location);
                location += offset * speed;
            }
            else
            {
                scale -= 0.05f;
                if (scale < 0) BeCollected();
            }
        }

        protected void BeCollected()
        {
            collector.Collect();
            ResourceManager.Remove(this);
        }

        public void Draw()
        {
            ScreenManager.SetTarget(display);
            Camera camera = ScreenManager.Camera(display);
            ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
            Drawer3D.Draw(model, location, rotation, new Vector3(scale * modelScale), opacity, camera);
        }

    }
}
