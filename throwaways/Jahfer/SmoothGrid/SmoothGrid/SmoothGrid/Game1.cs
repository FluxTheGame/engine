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

using System.Diagnostics;

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

        List<Vector3> points = new List<Vector3>();
        Vector3 pt1, pt2;
        VLine line;

        Stopwatch stopwatch = new Stopwatch();

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
            //Components.Add(new FrameRateCounter(this));

            VLine.Effect = new BasicEffect(GraphicsDevice);

            pt1 = new Vector3(0,  0.66f, 0);
            pt2 = new Vector3(0, -0.66f, 0);

            points.Add(new Vector3(0, 1.8f, 0));
            points.Add(pt1);
            points.Add(pt2);
            points.Add(new Vector3(0, -1.8f, 0));

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

            pt1.X += 0.01f;
            pt2.X -= 0.01f;
            points[1] = pt1;
            points[2] = pt2;

            line = new VLine(points, graphics.GraphicsDevice, 0.1f, 5000);
            line.curve();
            line.stroke();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            line.draw(camera);

            base.Draw(gameTime);
        }
    }
}
