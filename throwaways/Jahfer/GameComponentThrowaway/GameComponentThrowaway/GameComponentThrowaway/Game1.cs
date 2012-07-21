#define LOG_LEVEL_VERBOSE

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace GameComponentThrowaway {

    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // 3D model
        Model theMesh;
        Vector3 modelPosition = Vector3.Zero;
        float modelRotation = 0.0f;
        Vector3 modelVelocity = Vector3.Zero;
        Model secondMesh;

        // Shader classes
        Effect effect;
        EffectParameter projectionParameter;
        EffectParameter viewParameter;
        EffectParameter worldParameter;
        EffectParameter ambientIntensityParameter;
        EffectParameter ambientColorParameter;

        Matrix world, view, projection;
        float ambientLightIntensity;
        Vector4 ambientLightColor;

        Vector3 cameraPosition = new Vector3(0.0f, 50.0f, 5000.0f);


        // Object Managers
        UserManager     userManager;
        BiomeManager    biomeManager;
        AnimalManager   animalManager;
        PlantManager    plantManager;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            userManager     = new UserManager(this);
            biomeManager    = new BiomeManager(this);
            animalManager   = new AnimalManager(this);
            plantManager    = new PlantManager(this);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        public void SetupShaderParameters() {
            // bind the parameters with the shader
            worldParameter = effect.Parameters["World"];
            viewParameter = effect.Parameters["View"];
            projectionParameter = effect.Parameters["Projection"];

            ambientColorParameter = effect.Parameters["AmbientColor"];
            ambientIntensityParameter = effect.Parameters["AmbientIntensity"];
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);



            theMesh = Content.Load<Model>("Models\\p1_wedge");
            secondMesh = Content.Load<Model>("Models\\p1_wedge");
            // load the shader
            effect = Content.Load<Effect>("shader");
            // set up parameters
            SetupShaderParameters();

            // calculate matrices
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;

            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
            // create a default world matrix
            world = Matrix.Identity;

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            userManager.Update(gameTime);
            plantManager.Update(gameTime);
            animalManager.Update(gameTime);
            biomeManager.Update(gameTime);

            UpdateInput();
            modelPosition += modelVelocity;
            modelVelocity *= 0.95f;

            ambientLightIntensity = 1.0f;
            ambientLightColor = Color.DarkBlue.ToVector4();

            //userManager.createNewUser();
            //plantManager.createNewPlant();
            //animalManager.createNewAnimal();

            base.Update(gameTime);
        }

        protected void UpdateInput() {
            KeyboardState currentKeyState = Keyboard.GetState();

            if (currentKeyState.IsKeyDown(Keys.A))
                modelRotation += 0.1f;
            else if (currentKeyState.IsKeyDown(Keys.D))
                modelRotation -= 0.1f;

            // create some velocity if the enter key is down
            Vector3 modelVelocityAdd = Vector3.Zero;

            // find out what direction we should be thrusting, using rotation
            modelVelocityAdd.X = -(float)Math.Sin(modelRotation);
            modelVelocityAdd.Z = -(float)Math.Cos(modelRotation);

            // finally, add this vector to our velocty
            modelVelocity += modelVelocityAdd;

            // in case you get lost, press A to warp back to center
            if (currentKeyState.IsKeyDown(Keys.Enter)) {
                modelPosition = Vector3.Zero;
                modelVelocity = Vector3.Zero;
                modelRotation = 0.0f;
            }

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            ModelMesh mesh = theMesh.Meshes[0];
            ModelMeshPart meshPart = mesh.MeshParts[0];
            world = Matrix.CreateRotationY(modelRotation) * Matrix.CreateTranslation(modelPosition);

            // set parameters
            projectionParameter.SetValue(projection);
            viewParameter.SetValue(view);
            worldParameter.SetValue(world);
            ambientIntensityParameter.SetValue(ambientLightIntensity);
            ambientColorParameter.SetValue(ambientLightColor);

            // set the vertex source to the mesh's vertex buffer
            graphics.GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer, meshPart.VertexOffset);

            // set the current index buffer to the sample mesh's index buffer
            graphics.GraphicsDevice.Indices = meshPart.IndexBuffer;

            effect.CurrentTechnique = effect.Techniques["Technique1"];

            for (int i = 0; i < effect.CurrentTechnique.Passes.Count; i++) {
                // EffectPass.Apply will update the device to begin
                // using the state information defined in the current pass
                effect.CurrentTechnique.Passes[i].Apply();

                // theMesh contains all of the information required
                // to draw the current mesh
                graphics.GraphicsDevice.DrawIndexedPrimitives(
                    PrimitiveType.TriangleList, 0, 0,
                    meshPart.NumVertices, meshPart.StartIndex, meshPart.PrimitiveCount);
            }


            base.Draw(gameTime);
        }
    }
}
