using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking_002
{
    public class BasicEntity
    {

        public Model model;
        public Vector3 location;
        public float degRot = 0.0f;
        protected Matrix world = Matrix.Identity;

        public BasicEntity(Model mod)
        {
            model = mod;
        }

        public virtual void Update()
        {
        }

        public void Draw(Camera camera)
        {
            location /= 10;
            Matrix gameWorldRotation =
                Matrix.CreateRotationZ(MathHelper.ToRadians(degRot));

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            //draw the model
            //loop through the meshes in the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                //loop through the mesh layers
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.projection;
                    effect.View = camera.view;
                    effect.World = transforms[mesh.ParentBone.Index] * gameWorldRotation * Matrix.CreateScale(0.6f) * Matrix.CreateTranslation(location);
                }
                //draw the mesh after setting the effects on the mesh layers
                mesh.Draw();
            }
        }

        public virtual Matrix GetWorld()
        {
            return world;
        }

    }
}
