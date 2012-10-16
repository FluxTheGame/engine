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
        MouseState mouseState;
        SpriteBatch spriteBatch;

        GridObjectController gridObjectController;

        Vector2 mousePos;

        Texture2D icon;
        Texture2D mouseIcon;
        SpriteFont font;

        VectorField vf;

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
            vf = new VectorField(
                GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height,
                100, 100
            );

            EventManager.Initialize();

            EventManager.On("user:touch", (o) =>
            {
                Console.Write("on user:touch, args: ");
                Console.WriteLine((Double)o["x"]+26);
            });
            server = new Server();

            gridObjectController = new GridObjectController(this, vf);
            Components.Add(gridObjectController);

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            icon = Content.Load<Texture2D>("box");
            mouseIcon = Content.Load<Texture2D>("mouse");
            font = Content.Load<SpriteFont>("font");
        }

      
        protected override void UnloadContent()
        {
           
        }

       
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            mouseState = Mouse.GetState();
            mousePos = new Vector2(mouseState.X, mouseState.Y);

            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Down))
            {
                vf.addForceCircle(mousePos, 80.0f, 0.2f, true);
            } if (keyState.IsKeyDown(Keys.Up))
            {
                vf.addForceCircle(mousePos, 80.0f, 0.2f, false);
            }

            vf.Update();

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            vf.Draw(spriteBatch, font);

            spriteBatch.Draw(mouseIcon, mousePos, Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }

    }
}
