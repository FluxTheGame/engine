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

namespace Lighting
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Model mountain;
        MouseState originalMouseState;

        Matrix lightsViewProjectionMatrix;
        Vector3 diffuseLightDirection;

        RenderTarget2D renderTarget;
        Texture2D shadowMap;

        Effect lights, shadows;

        Matrix view, projection;

        Vector3 cameraPosition = new Vector3(0, 30, -50);
        float leftrightRot = MathHelper.PiOver2;
        float updownRot = -MathHelper.Pi / 10.0f;
        const float rotationSpeed = 0.3f;
        const float moveSpeed = 30.0f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 512;
            graphics.PreferredBackBufferHeight = 512;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferMultiSampling = true;

            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4,
                (float)Window.ClientBounds.Width / 
                (float)Window.ClientBounds.Height,
                1, 10000);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            originalMouseState = Mouse.GetState();

            mountain = Content.Load<Model>(@"Mountain");
            lights = Content.Load<Effect>(@"lighting");

            lights.CurrentTechnique = lights.Techniques["Technique1"];

            lights.Parameters["AmbientColor"].SetValue(Color.LawnGreen.ToVector4());
            lights.Parameters["AmbientIntensity"].SetValue(0.3f);
            lights.Parameters["DiffuseColor"].SetValue(Color.LightYellow.ToVector4());
            lights.Parameters["DiffuseIntensity"].SetValue(0.3f);
            lights.Parameters["SpecularColor"].SetValue(Color.Yellow.ToVector4());



            shadows = Content.Load<Effect>(@"shadows");
            shadows.CurrentTechnique = shadows.Techniques["ShadowMap"];

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            renderTarget = new RenderTarget2D(
                GraphicsDevice, 
                pp.BackBufferWidth, 
                pp.BackBufferHeight, 
                true, 
                GraphicsDevice.DisplayMode.Format, 
                DepthFormat.Depth24);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            float timeDifference = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0f;

            MouseState currentMouseState = Mouse.GetState();
            if (currentMouseState != originalMouseState)
            {
                float xDifference = currentMouseState.X - originalMouseState.X;
                float yDifference = currentMouseState.Y - originalMouseState.Y;
                leftrightRot -= rotationSpeed * xDifference * timeDifference;
                updownRot -= rotationSpeed * yDifference * timeDifference;
                Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                UpdateViewMatrix();
            }

            UpdateLightData(gameTime);

            base.Update(gameTime);
        }

        private void UpdateLightData(GameTime gameTime)
        {

            diffuseLightDirection =
                new Vector3(
                    0,
                    (float)Math.Sin(gameTime.TotalGameTime.TotalSeconds),
                    (float)Math.Cos(gameTime.TotalGameTime.TotalSeconds)
                );

            Matrix lightsView = Matrix.CreateLookAt(diffuseLightDirection, new Vector3(-2, -3, -10), new Vector3(0, 1, 0));
            Matrix lightsProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, 1f, 5f, 100f);

            lightsViewProjectionMatrix = lightsView * lightsProjection;
        }

        private void UpdateViewMatrix()
        {
            Matrix cameraRotation = Matrix.CreateRotationX(updownRot) * Matrix.CreateRotationY(leftrightRot);

            Vector3 cameraOriginalTarget = new Vector3(0, 0, -1);
            Vector3 cameraRotatedTarget = Vector3.Transform(cameraOriginalTarget, cameraRotation);
            Vector3 cameraFinalTarget = cameraPosition + cameraRotatedTarget;

            Vector3 cameraOriginalUpVector = new Vector3(0, 1, 0);
            Vector3 cameraRotatedUpVector = Vector3.Transform(cameraOriginalUpVector, cameraRotation);

            view = Matrix.CreateLookAt(cameraPosition, cameraFinalTarget, cameraRotatedUpVector);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);




            // ====| Setup lights =======================
            Matrix world = Matrix.Identity;

            lights.Parameters["World"].SetValue(world);
            Vector3 vecEye = new Vector3(0.0f, 50.0f, 5000.0f);
            lights.Parameters["EyePosition"].SetValue(vecEye);
            lights.Parameters["View"].SetValue(view);
            lights.Parameters["Projection"].SetValue(projection);

            // ensure the light direction is normalized, or
            // the shader will give some weird results
            diffuseLightDirection.Normalize();
            lights.Parameters["LightDirection"].SetValue(diffuseLightDirection);



            // ====| render shadow map ===================
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            GraphicsDevice.RasterizerState = rs;


            shadows.CurrentTechnique = shadows.Techniques["ShadowMap"];
            shadows.Parameters["xLightsWorldViewProjection"].SetValue(Matrix.Identity * lightsViewProjectionMatrix);

            foreach (ModelMesh mesh in mountain.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // set the vertex source to the mesh's vertex buffer
                    GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer, meshPart.VertexOffset);
                    // set the current index buffer to the sample mesh's index buffer
                    GraphicsDevice.Indices = meshPart.IndexBuffer;

                    foreach (EffectPass pass in shadows.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        GraphicsDevice.DrawIndexedPrimitives(
                           PrimitiveType.TriangleList, 0, 0,
                           meshPart.NumVertices, meshPart.StartIndex, meshPart.PrimitiveCount);
                    }
                }
            }

            // ====| render scene with shadows ============
            GraphicsDevice.SetRenderTarget(null);
            shadowMap = (Texture2D)renderTarget;
            
            GraphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            shadows.CurrentTechnique = shadows.Techniques["ShadowedScene"];
            shadows.Parameters["xWorldViewProjection"].SetValue(Matrix.Identity * view * projection);
            shadows.Parameters["xShadowMap"].SetValue(shadowMap);

            foreach (ModelMesh mesh in mountain.Meshes)
            {
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    // set the vertex source to the mesh's vertex buffer
                    GraphicsDevice.SetVertexBuffer(meshPart.VertexBuffer, meshPart.VertexOffset);
                    // set the current index buffer to the sample mesh's index buffer
                    GraphicsDevice.Indices = meshPart.IndexBuffer;

                    foreach (EffectPass pass in shadows.CurrentTechnique.Passes)
                    {
                        pass.Apply();

                        GraphicsDevice.DrawIndexedPrimitives(
                           PrimitiveType.TriangleList, 0, 0,
                           meshPart.NumVertices, meshPart.StartIndex, meshPart.PrimitiveCount);
                    }
                }
            }

            shadowMap = null;

            base.Draw(gameTime);
        }
    }
}
