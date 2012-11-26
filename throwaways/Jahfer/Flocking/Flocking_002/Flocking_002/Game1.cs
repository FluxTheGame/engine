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
        public List<Boid> entities = new List<Boid>();

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {

            //initialize and add the Camera object
            camera = new Camera(this, new Vector3(0.0f, 0.0f, 50.0f), Vector3.Zero, Vector3.Up);
            Components.Add(camera);

            base.Initialize();

        }

        protected override void LoadContent()
        {
            for (int i = 0; i < 100; i++)
            {
                Boid b = new Boid(Vector3.Zero);
                b.LoadContent(Content);
                entities.Add(b);
            }
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


            //run through all entities and run their Update functions
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].run(entities);
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            foreach (Boid b in entities)
            {
                b.render(camera);
            }

            base.Draw(gameTime);
        }

    }

}
