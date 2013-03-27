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

        protected Vector3 origLocation;
        protected float speed;
        protected Model model;
        protected Schedualizer respawnDelay;

        public Resource(Vector3 location, string modelName, float speed = 0.05f)
        {
            model = ContentManager.Model(modelName);
            respawnDelay = new Schedualizer(0, 3, 5);
            this.origLocation = location;
            this.speed = speed;
            Initialize();
        }

        public void Initialize()
        {
            location = origLocation;
            collector = null;
        }

        public virtual void Update()
        {
            Matrix proj = ScreenManager.Camera(display).projection;
            Matrix view = ScreenManager.Camera(display).view;
            Vector3 screenPos = ScreenManager.graphics.Viewport.Project(location, proj, view, Matrix.Identity);
            position = new Vector2(screenPos.X, screenPos.Y);     
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

        protected float CollectorDistance()
        {
            return Vector2.Distance(position, collector.position);
        }

        protected Vector3 GetIntensity(Vector3 aim, Vector3 loc)
        {
            float dist = Vector3.Distance(aim, NonDepthLocation());
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
