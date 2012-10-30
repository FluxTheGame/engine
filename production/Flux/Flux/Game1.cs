using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GridObjectManager gridObjectManager;
        UserManager userManager;
        ResourceManager resourceManager;

        Texture2D icon;
        SpriteFont font;

        Server server;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 700;
            graphics.PreferredBackBufferHeight = 700;
            Content.RootDirectory = "Content";
        }

       
        protected override void Initialize()
        {
            EventManager.Initialize();
            GridManager.Initialize(
                GraphicsDevice.Adapter.CurrentDisplayMode.Width, 
                GraphicsDevice.Adapter.CurrentDisplayMode.Height,
                9
            );
            server = new Server();

            gridObjectManager = new GridObjectManager(this);
            Components.Add(gridObjectManager);

            userManager = new UserManager(this);
            Components.Add(userManager);

            resourceManager = new ResourceManager(this);
            Components.Add(resourceManager);

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            icon = Content.Load<Texture2D>("box");
            font = Content.Load<SpriteFont>("font");
        }

      
        protected override void UnloadContent()
        {
           
        }

       
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            GridManager.Update();

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            GridManager.Draw(spriteBatch, font);
            spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
