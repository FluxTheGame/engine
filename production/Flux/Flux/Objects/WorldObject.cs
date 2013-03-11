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
        static float WorldScale = 1;

        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
        public int display;

        public BoundingBox box;

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
            CalculateBoundingBox();

        }


        public void Update()
        {

        }

        public Vector3 Scale()
        {
            return scale * WorldScale;
        }

        public Vector3 Location()
        {
            //return (position * 1.5f) + new Vector3(0, 2, 9);// * WorldScale;
            return position;
        }


        protected void CalculateBoundingBox()
        {
            // Initialize minimum and maximum corners of the bounding box to max and min values
            Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

            // For each mesh of the model
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // Vertex buffer parameters
                    int vertexStride = meshPart.VertexBuffer.VertexDeclaration.VertexStride;
                    int vertexBufferSize = meshPart.NumVertices * vertexStride;

                    // Get vertex data as float
                    float[] vertexData = new float[vertexBufferSize / sizeof(float)];
                    meshPart.VertexBuffer.GetData<float>(vertexData);

                    // Iterate through vertices (possibly) growing bounding box, all calculations are done in world space
                    for (int i = 0; i < vertexBufferSize / sizeof(float); i += vertexStride / sizeof(float))
                    {
                        Vector3 transformedPosition = Vector3.Transform(new Vector3(vertexData[i], vertexData[i + 1], vertexData[i + 2]), Matrix.Identity);

                        min = Vector3.Min(min, transformedPosition);
                        max = Vector3.Max(max, transformedPosition);
                    }
                }
            }

            // Create and return the model bounding box
            box = new BoundingBox(min*scale, max*scale);
        }

        public void Draw()
        {
            Camera c = ScreenManager.Camera(display);
            Drawer3D.Draw(model, Location(), rotation, Scale(), 1, c);
        }

    }

}
