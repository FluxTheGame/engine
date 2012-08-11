using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameComponentThrowaway {

    public enum AnimalType {
        HERBIVORE,
        CARNIVORE,
        OMNIVORE
    };


    /// <summary>
    /// 
    /// </summary>
    class Animal: EventArgs {

        /// <summary>
        /// The current "state" of the <c>Animal</c>. The feeding state can be of
        /// the following types: attack, graze, drink; the type depends on the nature
        /// of the animal.
        /// </summary>
        public enum AnimalState {
            BIRTH,
            DEATH,
            FEED // -> attack, graze or drink depending on type
        };

        // Constructor
        public Animal(ContentManager content) {
            this.ID = getID();
            this.Type = AnimalType.OMNIVORE;
            this.State = AnimalState.BIRTH;

            _modelPosition = new Vector3(rand.Next(1000), rand.Next(1000), rand.Next(1000));

            theModel = content.Load<Model>("Models\\p1_wedge");
        }

        private static int userID = 0;
        public static int getID() {
            return userID++;
        }

        public int ID { get; private set; }
        
        // 3D model
        private Model theModel;

        private Random rand = new Random();

        /// <summary>
        /// The <c>State</c> property represents the current state of the 
        /// animal on-screen.
        /// </summary>
        public AnimalState State { get; private set; }

        /// <summary>
        /// The designated type of the <c>Animal</c>. This property controls the behaviours
        /// of the <c>Animal</c> when interacting with the biome and other <c>Animal</c>s.
        /// </summary>
        public AnimalType Type { get; private set; }

        /// <summary>
        /// The <c>Position</c> element property represents the current on-screen
        /// coordinates of the animal.
        /// </summary>
        private Vector3 _modelPosition;
        private Vector3 _modelVelocity   = Vector3.Zero;
        private float _modelRotation = 0.0f;

        public void Update() {
            KeyboardState currentKeyState = Keyboard.GetState();

            if (currentKeyState.IsKeyDown(Keys.A))
                _modelRotation += 0.1f;
            else if (currentKeyState.IsKeyDown(Keys.D))
                _modelRotation -= 0.1f;

            // create some velocity if the enter key is down
            Vector3 modelVelocityAdd = Vector3.Zero;

            // find out what direction we should be thrusting, using rotation
            modelVelocityAdd.X = -(float)Math.Sin(_modelRotation);
            modelVelocityAdd.Z = -(float)Math.Cos(_modelRotation);

            // finally, add this vector to our velocty
            _modelVelocity += modelVelocityAdd;

            // in case you get lost, press A to warp back to center
            if (currentKeyState.IsKeyDown(Keys.Enter)) {
                _modelPosition = Vector3.Zero;
                _modelVelocity = Vector3.Zero;
                _modelRotation = 0.0f;
            }

            _modelPosition += _modelVelocity;
            _modelVelocity *= 0.95f;
        }

        public void Draw(Game game, Effect effect) {
            //Console.WriteLine("User {0} has been drawn!", this.ID);
            Matrix[] transforms = new Matrix[theModel.Bones.Count];
            theModel.CopyAbsoluteBoneTransformsTo(transforms);

            Vector3 diffuseLightDirection = new Vector3(0, -1, -1);

            // ensure the light direction is normalized, or
            // the shader will give some weird results
            diffuseLightDirection.Normalize();
            effect.Parameters["LightDirection"].SetValue(diffuseLightDirection);

            Matrix world = Matrix.CreateRotationY(_modelRotation) * Matrix.CreateTranslation(_modelPosition);

            effect.Parameters["World"].SetValue(world);
            Vector3 vecEye = new Vector3(0.0f, 50.0f, 5000.0f);
            effect.Parameters["EyePosition"].SetValue(vecEye);

            foreach (ModelMesh mesh in theModel.Meshes) {
                foreach (ModelMeshPart meshPart in mesh.MeshParts) {
                    // set the vertex source to the mesh's vertex buffer
                    game.GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer, meshPart.VertexOffset);
                    // set the current index buffer to the sample mesh's index buffer
                    game.GraphicsDevice.Indices = meshPart.IndexBuffer;

                    foreach (EffectPass pass in effect.CurrentTechnique.Passes) {
                        pass.Apply();

                        game.GraphicsDevice.DrawIndexedPrimitives(
                           PrimitiveType.TriangleList, 0, 0,
                           meshPart.NumVertices, meshPart.StartIndex, meshPart.PrimitiveCount);
                    }
                }
            }
        }
    }
}
