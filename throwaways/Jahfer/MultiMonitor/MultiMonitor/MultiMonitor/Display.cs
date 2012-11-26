using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MultiMonitor
{
    public class Display
    {
        public GraphicsDevice Graphics;
        private ContentManager Content;
        public UInt16 UID;

        private Model player;
        private float aspectRatio;

        Vector3 modelPosition = Vector3.Zero;
        float modelRotation = 0.0f;

        public Display(UInt16 index, ContentManager iContent, GraphicsAdapter graphicsAdapter, GraphicsProfile graphicsProfile, PresentationParameters presentationParameters)
        {
            UID = index;
            Graphics = new GraphicsDevice(graphicsAdapter, graphicsProfile, presentationParameters);

            aspectRatio = Graphics.Viewport.AspectRatio;

            Content = iContent;
        }

        public void LoadContent(ContentManager content)
        {
            player = Content.Load<Model>(@"Model\player");
        }

        public void Draw() {

            Console.WriteLine("Drawing monitor #" + UID);

            Graphics.Clear(Color.YellowGreen);

            Console.WriteLine("0");

            if (player != null)
            {
                Console.WriteLine("1");
                // Copy any parent transforms.
                Matrix[] transforms = new Matrix[player.Bones.Count];
                player.CopyAbsoluteBoneTransformsTo(transforms);
                Console.WriteLine("2");

                // Draw the model. A model can have multiple meshes, so loop.
                foreach (ModelMesh mesh in player.Meshes)
                {
                    // This is where the mesh orientation is set, as well 
                    // as our camera and projection.
                    foreach (BasicEffect effect in mesh.Effects)
                    {
                        effect.EnableDefaultLighting();
                        effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateRotationY(modelRotation) * Matrix.CreateTranslation(modelPosition);
                        effect.View = Matrix.CreateLookAt(Camera.Position, Vector3.Zero, Vector3.Up);
                        effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
                    }
                    // Draw the mesh, using the effects set above.
                    mesh.Draw();
                }
                Console.WriteLine("3");
            }

            Console.WriteLine("4");
            Graphics.Present();

            Console.WriteLine("Finished drawing monitor #" + UID);
        }
    }
}
