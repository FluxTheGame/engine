using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking_002
{
    class BasicEntity
    {

        public Model model;
        public Vector3 location;
        protected Matrix world = Matrix.Identity;

        public BasicEntity(Model mod)
        {

            model = mod;

        }//end BasicEntity

        public virtual void Update()
        {

            //TODO

        }//end Update

        public void Draw(Camera camera)
        {

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
                    effect.World = GetWorld() * mesh.ParentBone.Transform;

                }

                //draw the mesh after setting the effects on the mesh layers
                mesh.Draw();

            }


        }//end Draw

        public virtual Matrix GetWorld()
        {

            return world;

        }//end GetWorld

    }
}
