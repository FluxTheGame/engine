
//#define FOUR_SCREENS

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
        Vector2 initialMousePos = Vector2.Zero;

        int currentDisplay = 0;
        Ground ground;

        CollectorManager collectorManager;
        WorldManager worldManager;
        EnemyManager enemyManager;
        UserManager userManager;
        ResourceManager resourceManager;
        WormholeManager wormholeManager;

        Server server;

        KeyboardState oldState; //For detecting key presses
        SpriteBatch spriteBatch;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1280;

            #if FOUR_SCREENS
                graphics.PreferredBackBufferHeight = 200;
            #else
                graphics.PreferredBackBufferHeight = 800;
            #endif

            graphics.PreferMultiSampling = true;

            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);

            Content.RootDirectory = "Content";
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            PresentationParameters pp = e.GraphicsDeviceInformation.PresentationParameters;
            pp.MultiSampleCount = 16;

            return;
        }

       
        protected override void Initialize()
        {
            ScreenManager.Initialize(this, graphics.GraphicsDevice);

            ContentManager.Initialize(this);
            EventManager.Initialize();
            GridManager.Initialize(10);
            server = new Server();

            collectorManager = new CollectorManager(this);
            Components.Add(collectorManager);

            worldManager = new WorldManager(this);
            Components.Add(worldManager);
            
            /*enemyManager = new EnemyManager(this);
            Components.Add(enemyManager);

            resourceManager = new ResourceManager(this);
            Components.Add(resourceManager);

            wormholeManager = new WormholeManager(this);
            Components.Add(wormholeManager);*/

            userManager = new UserManager(this);
            Components.Add(userManager);

            VLine.Effect = new BasicEffect(GraphicsDevice);

            oldState = Keyboard.GetState();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            ground = new Ground();

            /* For Testing */
            //Add Collector
            
            OrderedDictionary c = new OrderedDictionary();
            c.Add("id", 1);
            EventManager.Emit("collector:new", c);
            
            //Add User
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 99);
            o.Add("teamId", 1);
            o.Add("username", "DILBERT");
            EventManager.Emit("user:new", o);
            /* End for testing */
        }

      
        protected override void UnloadContent()
        {
           
        }

       
        protected override void Update(GameTime gameTime)
        {

            //if (gameTime.IsRunningSlowly) Console.WriteLine("SLOWLY");
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            GridManager.Update();

            /* For Testing - Accepts keyboard/mouse */
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 99);

            if (keyState.IsKeyDown(Keys.Up))
            {
                if (!oldState.IsKeyDown(Keys.Up))
                {
                    EventManager.Emit("user:bloat", o);
                }
            }
            else if (oldState.IsKeyDown(Keys.Up))
            {
                EventManager.Emit("user:bloatEnd", o);
            }

            if (keyState.IsKeyDown(Keys.Down))
            {
                if (!oldState.IsKeyDown(Keys.Down)) 
                {
                    EventManager.Emit("user:pinch", o);
                }
            }
            else if (oldState.IsKeyDown(Keys.Down))
            {
                EventManager.Emit("user:pinchEnd", o);
            }

            if (keyState.IsKeyDown(Keys.Space))
            {
                if (!oldState.IsKeyDown(Keys.Space))
                {
                    //o.Add("type", "wet");
                    //o.Add("value", 582);
                    //EventManager.Emit("user:getbadge", o);
                    //EventManager.Emit("user:getpoints", o);
                    //EventManager.Emit("user:disconnect", o);

                    OrderedDictionary c = new OrderedDictionary();
                    c.Add("id", 1);
                    EventManager.Emit("collector:attack", c);
                }
            }

            if (keyState.IsKeyDown(Keys.D1)) 
            {
                currentDisplay = 0;
            } 
            else if (keyState.IsKeyDown(Keys.D2)) 
            {
                currentDisplay = 1;
            } else if (keyState.IsKeyDown(Keys.D3)) {
                currentDisplay = 2;
            } else if (keyState.IsKeyDown(Keys.D4)) {
                currentDisplay = 3;
            }

            oldState = keyState;

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

            //Camera update for movement
            ScreenManager.Update(gameTime);

            /* End for testing */

            
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                ScreenManager.SetTarget(i);
                ScreenManager.graphics.Clear(Color.Transparent);
            }
            // Draw Ground
            ground.Draw();

            // Draw Grid
            GridManager.Draw();

            base.Draw(gameTime);



            ScreenManager.graphics.SetRenderTarget(null);
            ScreenManager.graphics.Clear(Color.Black);

            ScreenManager.spriteBatch.Begin();

#if FOUR_SCREENS
            float scale = 0.25f;
            int frameWidth = (int)(ScreenManager.window.X / 4);

            for (int i = 0; i < 4; i++)
            {
                ScreenManager.spriteBatch.Draw(
                    (Texture2D)ScreenManager.Target(i), new Vector2(frameWidth * i, 0), 
                    ScreenManager.Target(i).Bounds, Color.White,
                    0, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            }
#else
            ScreenManager.spriteBatch.Draw(
                    (Texture2D)ScreenManager.Target(currentDisplay), new Vector2(0, 0), 
                    ScreenManager.Target(0).Bounds, Color.White,
                    0, new Vector2(0, 0), 1, SpriteEffects.None, 0f);
#endif
            
            ScreenManager.spriteBatch.End();
        }

    }
}
