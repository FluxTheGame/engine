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

        public Vector2 position;
        public int display;
        public Model model;
        protected DateTime created;

        public GameObject()
        {
            position = ScreenManager.Middle();
            display = 0;
            created = DateTime.Now;
        }

        public virtual void Update()
        {
        }

        public virtual Vector3 Location()
        {
            return ScreenManager.Location(position);
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
                        effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(0.1f) * Matrix.CreateTranslation(Location());
                    }
                    mesh.Draw();
                }
            }
        }


    }
}
