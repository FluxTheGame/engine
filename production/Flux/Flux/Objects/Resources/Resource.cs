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
        public float scale = 1f;
        public float opacity = 1f;
        public Collector collector;

        protected float speed;
        protected float modelScale = 0.1f;
        protected Model model;


        public Resource(Vector3 location, string modelName, float speed)
        {
            model = ContentManager.Model(modelName);
            this.speed = speed;
            this.location = location;
        }

        public virtual void Update()
        {
            if (collector != null)
            {
                if (Vector2.Distance(Position(), collector.position) < 30)
                {
                    collector.Collect();
                    ResourceManager.Remove(this);
                }
                else
                {
                    MoveToCollector();
                }
            }
        }

        public virtual void MoveToCollector()
        {
            //Override this method to implement resource movement logic

            Vector3 offset = (collector.Location() - location);
            location += offset * speed;
        }

        public Vector2 Position()
        {
            return ScreenManager.ProjectedPosition(location, display);
        }

        public void Draw()
        {
            ScreenManager.SetTarget(display);
            Camera camera = ScreenManager.Camera(display);
            ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
            Drawer3D.Draw(model, location, scale * modelScale, opacity, camera);
        }

    }
}
