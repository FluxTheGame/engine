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
        SpriteFont font;
        Vector2 initialMousePos = Vector2.Zero;

        CollectorManager collectorManager;
        EnemyManager enemyManager;
        UserManager userManager;
        ResourceManager resourceManager;
        WormholeManager wormholeManager;

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
            ContentManager.Initialize(this);
            EventManager.Initialize();
            GridManager.Initialize(
                GraphicsDevice.Viewport.Width, 
                GraphicsDevice.Viewport.Height,
                10
            );
            server = new Server();

            collectorManager = new CollectorManager(this);
            Components.Add(collectorManager);
            
            enemyManager = new EnemyManager(this);
            Components.Add(enemyManager);

            userManager = new UserManager(this);
            Components.Add(userManager);

            resourceManager = new ResourceManager(this);
            Components.Add(resourceManager);

            wormholeManager = new WormholeManager(this);
            Components.Add(wormholeManager);

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("font");

            /* For Testing */
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 1);
            o.Add("username", "Matt");
            EventManager.Emit("user:join", o);
            /* End for testing */
        }

      
        protected override void UnloadContent()
        {
           
        }

       
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            GridManager.Update();

            /* For Testing - Accepts keyboard/mouse */
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 1);

            if (keyState.IsKeyDown(Keys.Up))
            {
                EventManager.Emit("user:bloat", o);
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                EventManager.Emit("user:pinch", o);
            }

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                if (initialMousePos.Equals(Vector2.Zero))
                {
                    initialMousePos = new Vector2(mouseState.X, mouseState.Y);
                }
                o.Add("x", (int)(mouseState.X - initialMousePos.X));
                o.Add("y", (int)(mouseState.Y - initialMousePos.Y));
                EventManager.Emit("user:touch", o);
            }
            else if (mouseState.LeftButton == ButtonState.Released)
            {
                if (initialMousePos.Equals(Vector2.Zero))
                {
                    EventManager.Emit("user:touchEnd", o);
                }
                initialMousePos = Vector2.Zero;
            }
            /* End for testing */

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
