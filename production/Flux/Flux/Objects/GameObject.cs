using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class GameObject
    {

        public float scale;
        public Vector2 position;
        public int display;
        public Model model;
        protected DateTime created;

        public GameObject()
        {
            scale = 0.1f;
            position = ScreenManager.Middle();
            display = 0;
            created = DateTime.Now;
        }

        public virtual void Update()
        {
            if (position.X > ScreenManager.window.X) position.X = ScreenManager.window.X;
            if (position.Y > ScreenManager.window.Y) position.Y = ScreenManager.window.Y;
            if (position.X < 0) position.X = 0;
            if (position.Y < 0) position.Y = 0;
        }

        public virtual Vector3 Location()
        {
            return ScreenManager.Location(position, display);
        }

        public virtual void Draw()
        {
            if (model != null) {

                Camera camera = ScreenManager.Camera(display);
                Matrix[] transforms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(transforms);
                
                foreach (ModelMesh mesh in model.Meshes) {
                    foreach (BasicEffect effect in mesh.Effects) {
                        effect.EnableDefaultLighting();
                        effect.Projection = camera.projection;
                        effect.View = camera.view;
                        effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(scale) * Matrix.CreateTranslation(Location());
                    }
                    mesh.Draw();
                }
            }
        }


    }
}
