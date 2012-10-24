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

namespace Flocking_002
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {

        GraphicsDeviceManager graphics;
        public Camera camera;
        ModelManger modelManager;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {

            //initialize and add the Camera object
            camera = new Camera(this, new Vector3(0, 0, 50), Vector3.Zero, Vector3.Up);
            Components.Add(camera);
            modelManager = new ModelManger(this);
            Components.Add(modelManager);
            
            base.Initialize();

        }

        protected override void LoadContent()
        {

            

        }

        protected override void UnloadContent()
        {
            


        }

        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyStates = Keyboard.GetState();

            // Allows the game to exit
            if (keyStates.IsKeyDown(Keys.Escape))
            {

                this.Exit();

            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);

        }

    }

}
