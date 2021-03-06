﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Drawer3D
    {
        public static BasicEffect DefaultLights = SetDefaultLights();

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

        public static BasicEffect SetDefaultLights()
        {
            BasicEffect effect = new BasicEffect(ScreenManager.graphics);
            return effect;
        }


        public static void Draw(Model model, Vector3 location, Vector3 rotation, Vector3 scale, float opacity, Camera camera)
        {
            Draw(model, location, rotation, scale, opacity, camera, null);
        }

        public static void Draw(Model model, Vector3 location, Vector3 rotation, Vector3 scale, float opacity, Camera camera, BasicEffect inEffect)
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
                            Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
                            Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
                            Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z)) *
                            Matrix.CreateScale(scale) * 
                            Matrix.CreateTranslation(location);

                        /*effect.DirectionalLight0.DiffuseColor = new Vector3(0.1f, 0.2f, 0.1f); // a red light
                        effect.DirectionalLight0.Direction = new Vector3(-1, -1.5f, -1);  // coming along the x-axis
                        effect.DirectionalLight0.SpecularColor = new Vector3(0.2f, 0.5f, 0.2f); // with green highlights*/

                        effect.DirectionalLight0.DiffuseColor = new Vector3(0.6f, 0.6f, 0.6f);
                        effect.DirectionalLight0.Direction = new Vector3(-1, 0, -1);
                        effect.DirectionalLight0.SpecularColor = new Vector3(1, 1, 1);
                        effect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.5f);

                        effect.DirectionalLight1.Direction = new Vector3(1, -1, 0);


                        if (inEffect != null)
                        {
                            //effect.DirectionalLight2.DiffuseColor = inEffect.DirectionalLight2.DiffuseColor;
                            //effect.DirectionalLight2.Direction = inEffect.DirectionalLight2.Direction;
                            //effect.DirectionalLight2.SpecularColor = inEffect.DirectionalLight2.SpecularColor;

                            effect.EmissiveColor = inEffect.EmissiveColor;
                            effect.AmbientLightColor = inEffect.AmbientLightColor;

                            //effect.AmbientLightColor = new Vector3(0.2f, 0.2f, 0.2f);
                            //effect.EmissiveColor = new Vector3(0.2f, 0.2f, 0.2f);
                        }

                        if (opacity < 1)
                        {
                            ScreenManager.graphics.BlendState = BlendState.AlphaBlend;
                        }
                        effect.Alpha = opacity;
                    }
                    model.Meshes[i].Draw();
                }
            }
        }
    }
}
