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

    public delegate void GenericEvent(object sender, EventArgs e);

    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Matrix world, view, projection;
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


        protected override void Initialize() {

            animalManager.AnimalCreatedEvent += new GenericEvent(AnimalCreatedCallback);

            base.Initialize();
        }

        // TESTING PURPOSES ONLY
        public void AnimalCreatedCallback(object sender, EventArgs e) {
            Console.WriteLine("> New Animal created...");
            Console.WriteLine(ObjectDumper.Dump(e));
        }


        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // load assets required in AnimalManager
            animalManager.LoadContent();

            // add two new animals
            animalManager.createNewAnimal();
            animalManager.createNewAnimal();

            // calculate matrices
            float aspectRatio = (float)graphics.GraphicsDevice.Viewport.Width / (float)graphics.GraphicsDevice.Viewport.Height;
            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), aspectRatio, 1.0f, 10000.0f);
            // create a default world matrix
            world = Matrix.Identity;

        }


        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }


        protected override void Update(GameTime gameTime) {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Update all component managers
            userManager.Update(gameTime);
            animalManager.Update(gameTime);
            plantManager.Update(gameTime);
            biomeManager.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // draw all animals inside of AnimalManager
            animalManager.Draw(gameTime, world, view, projection);

            base.Draw(gameTime);
        }
    }
}
