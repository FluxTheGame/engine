using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Drawer3D
    {
        public static void Draw(Model model, Vector3 location, Camera camera)
        {
            Draw(model, location, Vector3.Zero, 1, camera);
        }

        public static void Draw(Model model, Vector3 location, Vector3 rotation, Camera camera)
        {
            Draw(model, location, rotation, new Vector3(1), 1, camera);
        }

        public static void Draw(Model model, Vector3 location, float scale, float opacity, Camera camera)
        {
            Vector3 s = new Vector3(scale);
            Draw(model, location, Vector3.Zero, s, opacity, camera);
        }

        public static void Draw(Model model, Vector3 location, Vector3 scale, float opacity, Camera camera)
        {
            Draw(model, location, Vector3.Zero, scale, opacity, camera);
        }

        public static void Draw(Model model, Vector3 location, Vector3 rotation, Vector3 scale, float opacity, Camera camera)
        {
            if (model != null)
            {
                Matrix[] transforms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(transforms);

                for (int i = 0; i < model.Meshes.Count; i++)
                {
                    foreach (BasicEffect effect in model.Meshes[i].Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.Projection = camera.projection;
                        effect.View = camera.view;
                        effect.World = transforms[model.Meshes[i].ParentBone.Index] * 
                            Matrix.CreateScale(scale) * 
                            Matrix.CreateTranslation(location) *
                            Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
                            Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
                            Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));

                        effect.LightingEnabled = true; // turn on the lighting subsystem.

                        effect.DirectionalLight0.DiffuseColor = Light.SunDiffuseColor; // a red light
                        effect.DirectionalLight0.Direction = Light.SunDirection;  // coming along the x-axis
                        effect.DirectionalLight0.SpecularColor = Light.SunSpecularColor; // with green highlights

                        effect.DirectionalLight1.DiffuseColor = Color.DimGray.ToVector3();
                        effect.DirectionalLight1.Direction = new Vector3(0, -1, 0);
                        effect.DirectionalLight1.SpecularColor = Color.Black.ToVector3();

                        //effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
                        //effect.EmissiveColor = new Vector3(0.2f, 0.2f, 0.2f);

                        effect.Alpha = opacity;
                    }
                    model.Meshes[i].Draw();
                }
            }
        }
    }
}
