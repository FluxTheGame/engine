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

        protected float speed;
        protected float modelScale = 0.1f;
        protected Model model;
        protected Schedualizer respawnDelay;
        protected Vector3 originalLocation;

        public Resource(Vector3 location, string modelName, float speed = 0.05f)
        {
            model = ContentManager.Model(modelName);
            respawnDelay = new Schedualizer(0, 3, 5);
            this.speed = speed;
            this.location = location;
            this.originalLocation = location;
        }

        public virtual void Update()
        {
            if (respawnDelay.IsOn()) active = true;
        }

        public void GetCollected()
        {
            active = false;
            this.location = originalLocation;
        }

        public void Draw()
        {
            if (active)
            {
                ScreenManager.SetTarget(display);
                Camera camera = ScreenManager.Camera(display);
                ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
                Drawer3D.Draw(model, location, scale * modelScale, opacity, camera);
            }
        }

    }
}
