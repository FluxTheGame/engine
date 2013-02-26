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
        private Vector3 origLocation;
        int display = 0;

        Model model;
        float scale = 10.0f;
        public Vector3 target;
        private float speed = 0.01f;

        public Resource()
        {
            model = ContentManager.Model("chicken");
            scale = 0.01f;

            int x = Randomizer.RandomInt(-6, 50);
            int y = Randomizer.RandomInt(-4, 5);
            location = new Vector3(x, y, 0);

            target = location;
            origLocation = location;
        }

        public void Update()
        {
            Vector3 offsetFromTarget = GetIntensity(target, location);
            location += offsetFromTarget * speed;
        }

        private Vector3 GetIntensity(Vector3 aim, Vector3 loc)
        {
            float dist = Vector3.Distance(aim, loc);

            float intensity = 0f;
            if (dist > 0) intensity = 1.0f / (dist * dist); // inverse square

            Vector3 offset = (aim - loc) * intensity;

            return offset;
        }

        public void Draw()
        {
            if (model != null)
            {
                Camera camera = ScreenManager.Camera(display);
                Matrix[] transforms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.Projection = camera.projection;
                        effect.View = camera.view;
                        effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(scale) * Matrix.CreateTranslation(location);
                    }
                    mesh.Draw();
                }
            }
        }

    }
}
