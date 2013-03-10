using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;


namespace Flux
{

    public class WorldObject
    {
        static float WorldScale = 3;

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

            Matrix proj = ScreenManager.Camera(0).projection;
            Matrix view = ScreenManager.Camera(0).view;
            Vector3 screenPos = ScreenManager.graphics.Viewport.Project(position, proj, view, Matrix.Identity);

            float div = (int)(screenPos.X / ScreenManager.window.X);
            if (div <= 1) display = 0;
            else if (div <= 2) display = 1;
            else if (div <= 3) display = 2;
            else display = 3;

            model = mod;

        }


        public virtual void Update()
        {

        }

        public virtual Vector3 Scale()
        {
            return scale * WorldScale;
        }

        public virtual Vector3 Location()
        {
            return (position * 1.5f) + new Vector3(0, 2, 9);// * WorldScale;
        }


        public virtual void Draw()
        {

            if (model != null)
            {
                Matrix[] transforms = new Matrix[model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(transforms);

                foreach (ModelMesh mesh in model.Meshes)
                {
                    foreach (BasicEffect effect in mesh.Effects) {
                        Camera camera = ScreenManager.Camera(display);
                        effect.EnableDefaultLighting();
                        effect.Projection = camera.projection;
                        effect.View = camera.view;
                        effect.World = transforms[mesh.ParentBone.Index] *
                            Matrix.CreateScale(Scale()) *
                            Matrix.CreateTranslation(Location()) *
                            Matrix.CreateRotationX(MathHelper.ToRadians(rotation.X)) *
                            Matrix.CreateRotationY(MathHelper.ToRadians(rotation.Y)) *
                            Matrix.CreateRotationZ(MathHelper.ToRadians(rotation.Z));
                    }

                    mesh.Draw();
                }
                

            }

        }

    }

}
