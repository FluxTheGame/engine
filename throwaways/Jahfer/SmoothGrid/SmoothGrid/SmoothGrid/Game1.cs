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

namespace SmoothGrid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera camera;

        Matrix worldTranslation = Matrix.Identity;
        Matrix worldRotation = Matrix.Identity;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            camera = new Camera(this, new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
            Components.Add(camera);

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

            // TODO: use this.Content to load your game content here
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //VLine line = new VLine(new Vector3(-1, 1, 0), new Vector3(0, -1, 0), graphics.GraphicsDevice, 0.01f);
            //line.draw(camera);

            Vector3 anchorA = new Vector3(0, 1, 0);
            Vector3 anchorB = new Vector3(1, 0, 0);
            Vector3 ptA = new Vector3(0.2f, 0.9f, 0);
            Vector3 ptB = new Vector3(0.6f, 0.7f, 0);
            Vector3 ptC = new Vector3(0.8f, 0.1f, 0);

            Vector3 interpA = Vector3.CatmullRom(anchorA, ptA, ptB, anchorB, 0.5f);
            Vector3 interpB = Vector3.CatmullRom(anchorA, ptB, ptC, anchorB, 0.5f);

            float strokeWidth = 0.03f;

            VLine a = new VLine(anchorA, ptA, graphics.GraphicsDevice, strokeWidth);
            VLine b = new VLine(ptA, interpA, graphics.GraphicsDevice, strokeWidth);
            VLine c = new VLine(interpA, ptB, graphics.GraphicsDevice, strokeWidth);
            VLine d = new VLine(ptB, interpB, graphics.GraphicsDevice, strokeWidth);
            VLine e = new VLine(interpB, ptC, graphics.GraphicsDevice, strokeWidth);
            VLine f = new VLine(ptC, anchorB, graphics.GraphicsDevice, strokeWidth);

            a.draw(camera);
            b.draw(camera);
            c.draw(camera);
            d.draw(camera);
            e.draw(camera);
            f.draw(camera);


            base.Draw(gameTime);
        }
    }
}
