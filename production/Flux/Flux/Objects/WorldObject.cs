using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{

    public class WorldObject
    {
        static float worldScale = 0.5f;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public int display;

        public string objectName;
        public int objectGroup = -1;

        public float[] light = new float[4];
        public float[] shader = new float[4];
        public float shadow;
        public float[] misc = new float[4];

        public Model model;



        public WorldObject(Dictionary<string, string> args, Model mod)
        {

            objectName = args["objName"];

            int.TryParse(args["objGroup"], out objectGroup);

            float.TryParse(args["transX"], out position.X);
            float.TryParse(args["transY"], out position.Y);
            float.TryParse(args["transZ"], out position.Z);

            float.TryParse(args["rotateX"], out rotation.X);
            float.TryParse(args["rotateY"], out rotation.Y);
            float.TryParse(args["rotateZ"], out rotation.Z);

            float.TryParse(args["scaleX"], out scale.X);
            float.TryParse(args["scaleY"], out scale.Y);
            float.TryParse(args["scaleZ"], out scale.Z);

            float.TryParse(args["light1"], out light[0]);
            float.TryParse(args["light2"], out light[1]);
            float.TryParse(args["light3"], out light[2]);
            float.TryParse(args["light4"], out light[3]);

            float.TryParse(args["shader1"], out shader[0]);
            float.TryParse(args["shader2"], out shader[1]);
            float.TryParse(args["shader3"], out shader[2]);
            float.TryParse(args["shader4"], out shader[3]);

            float.TryParse(args["misc1"], out misc[0]);
            float.TryParse(args["misc2"], out misc[1]);
            float.TryParse(args["misc3"], out misc[2]);
            float.TryParse(args["misc4"], out misc[3]);

            float.TryParse(args["shadow"], out shadow);

            model = mod;

        }//end WorldObject


        public virtual void Update()
        {

        }//end Update


        public virtual Vector3 Location()
        {
            //Vector3 offset = new Vector3(-170.6382769f, -25.03253131f, 120.8359542f) * worldScale;
            return (position * worldScale);// - offset;
        }//end Location


        public virtual void Draw()
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
                        effect.World = transforms[mesh.ParentBone.Index] * 
                            Matrix.CreateScale(scale * worldScale) *
                            Matrix.CreateTranslation(Location()) * 
                            Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
                            Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) * 
                            Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
                    }

                    mesh.Draw();

                }

            }

        }//end Draw

    }

}
