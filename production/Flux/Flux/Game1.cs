
#define PRODUCTION

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

        bool gamePaused = false;

        RenderTarget2D[] tmpTargets = new RenderTarget2D[4];

        int currentDisplay = 0;
        Ground ground;

        CollectorManager collectorManager;
        WorldManager worldManager;
        EnemyManager enemyManager;
        UserManager userManager;
        ResourceManager resourceManager;
        WormholeManager wormholeManager;
        Effect beautyPass;
        Texture2D noiseTexture;

        Server server;

        KeyboardState oldState; //For detecting key presses
        MouseState oldMouseState; //For detecting mouse presses
        SpriteBatch spriteBatch;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;

            #if PRODUCTION
                graphics.PreferredBackBufferWidth = 1280 * 4;
            
            #else
                graphics.PreferredBackBufferWidth = 1280;
            #endif

                graphics.PreferMultiSampling = true;
            graphics.PreparingDeviceSettings += new EventHandler<PreparingDeviceSettingsEventArgs>(graphics_PreparingDeviceSettings);
            Content.RootDirectory = "Content";            
        }

        void graphics_PreparingDeviceSettings(object sender, PreparingDeviceSettingsEventArgs e)
        {
            PresentationParameters pp = e.GraphicsDeviceInformation.PresentationParameters;
            //pp.MultiSampleCount = 16;

            return;
        }

       
        protected override void Initialize()
        {
            ScreenManager.Initialize(this, graphics.GraphicsDevice);

            ContentManager.Initialize(this);
            EventManager.Initialize();
            GridManager.Initialize(10);
            Audio.Initialize();
            server = new Server();

            worldManager = new WorldManager(this);
            worldManager.DrawOrder = 1;
            Components.Add(worldManager);
            
            resourceManager = new ResourceManager(this, worldManager);
            resourceManager.DrawOrder = 2;
            Components.Add(resourceManager);

            wormholeManager = new WormholeManager(this);
            wormholeManager.DrawOrder = 3;
            Components.Add(wormholeManager);

            collectorManager = new CollectorManager(this);
            collectorManager.DrawOrder = 4;
            Components.Add(collectorManager);

            enemyManager = new EnemyManager(this);
            enemyManager.DrawOrder = 5;
            Components.Add(enemyManager);

            userManager = new UserManager(this);
            userManager.DrawOrder = 6;
            Components.Add(userManager);

            TeamColour.Initialize();
            VLine.Effect = new BasicEffect(GraphicsDevice);

            beautyPass = ContentManager.Shader("BeautyRender");
            noiseTexture = Content.Load<Texture2D>(@"images/noise");
            beautyPass.CurrentTechnique = beautyPass.Techniques["Pretty"];
            beautyPass.Parameters["TextureSize"].SetValue(ScreenManager.window);
            beautyPass.Parameters["NoiseMap"].SetValue(noiseTexture);

            oldState = Keyboard.GetState();
            spriteBatch = new SpriteBatch(GraphicsDevice);

            for (int i=0; i<4; i++)
            {
                tmpTargets[i] = new RenderTarget2D(ScreenManager.graphics, 1280, 800, false,
                ScreenManager.graphics.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 8, RenderTargetUsage.PreserveContents);
            }

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            ground = new Ground();
            CloudManager.LoadContent();

            // Load audio
            Audio.MultiSpeakerOutput_Load();

            Dictionary<string, string> audioFiles = new Dictionary<string, string>();
            audioFiles.Add("ambient.flux", "sfx/ambient/flux_v2.1.wav");
            Audio.Load(audioFiles);

            Audio.Play("ambient.flux", 0, 0.3f, true);
            Audio.Play("ambient.flux", 1, 0.3f, true);
            Audio.Play("ambient.flux", 2, 0.3f, true);
            Audio.Play("ambient.flux", 3, 0.3f, true);
            Audio.Play("ambient.flux", 4, 0.3f, true);

            //Preload spritesheets to reduce lag on first-time load
            Wormhole.WormholeAnimations();
            Collector.CollectorAnimations();


            /* For Testing */
            //Add Collector
#if PRODUCTION
#else
            OrderedDictionary c = new OrderedDictionary();
            c.Add("id", 0);
            EventManager.Emit("collector:new", c);

            //Add User
            
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 0);
            o.Add("teamId", 0);
            o.Add("username", "DILBERT");
            o.Add("display", 0);
            EventManager.Emit("user:new", o);
#endif
            
            /* End for testing */
        }
      
        protected override void UnloadContent()
        {
            Audio.Dispose();
        }

       
        protected override void Update(GameTime gameTime)
        {

            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || Mouse.GetState().MiddleButton == ButtonState.Pressed)
                this.Exit();

            if (mouseState.RightButton == ButtonState.Pressed && oldMouseState.RightButton == ButtonState.Released)
            {
                gamePaused = !gamePaused;
                Audio.Mute(gamePaused);
            }

            oldMouseState = mouseState;
            //if (gamePaused)  return;

            GridManager.Update();
            Tweenerizer.Update();
            Audio.Update();
            CloudManager.Update();

            /* For Testing - Accepts keyboard/mouse */
#if PRODUCTION
#else
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 0);

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
                    //o.Add("type", "theOcho");
                    //o.Add("value", 100);
                    o.Add("userId", 0);
                    EventManager.Emit("collector:attack", o);
                    //EventManager.Emit("user:getBadge", o);
                    //EventManager.Emit("user:getPoints", o);
                    //EventManager.Emit("user:disconnect", o);
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
#endif

            //Camera update for movement
            ScreenManager.Update(gameTime);
            Light.Update(gameTime);

            /* End for testing */

            oldState = keyState;
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            for (int i = 0; i < 4; i++)
            {
                ScreenManager.SetTarget(i);
                ScreenManager.graphics.Clear(Color.Transparent);
            }

            //Draw Ground Plane
            ground.Draw();
            CloudManager.Draw(gameTime);

            base.Draw(gameTime);

            GridManager.Draw();

            //Draw Grid

            ScreenManager.graphics.SetRenderTarget(null);
            ScreenManager.graphics.Clear(Color.SkyBlue);
            //ScreenManager.spriteBatch.Begin();

            #if PRODUCTION

                float scale = 1;
                int frameWidth = (int)(ScreenManager.window.X * scale);
                ScreenManager.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, RasterizerState.CullNone, beautyPass);

                for (int i = 0; i < 2; i++)
                {
                    ScreenManager.graphics.SetRenderTarget(tmpTargets[i]);
                    // draw texture with shader applied
                    ScreenManager.spriteBatch.Draw(ScreenManager.Target(i), ScreenManager.screenRect, Color.White);
                }

                ScreenManager.graphics.SetRenderTarget(null);

                for (int i = 0; i < 4; i++)
                {
                    ScreenManager.spriteBatch.Draw(
                        ScreenManager.Target(i), new Vector2(frameWidth * i, 0),
                        ScreenManager.Target(i).Bounds, Color.White,
                        0, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
                }
                ScreenManager.spriteBatch.End();
#else
            RenderTarget2D tex = Shaderizer._drawShader(ScreenManager.Target(currentDisplay), Shaderizer.tmpTarget, beautyPass, false);
            Shaderizer._renderTexture(-1, tex);

                //Shaderizer.Draw2D(currentDisplay, WormholeManager.SwirlShader);
                /*ScreenManager.spriteBatch.Draw(
                        (Texture2D)ScreenManager.Target(currentDisplay), new Vector2(0, 0),
                        ScreenManager.Target(currentDisplay).Bounds, Color.White,
                        0, new Vector2(0, 0), 1, SpriteEffects.None, 0f);*/
#endif

            //ScreenManager.spriteBatch.End();
        }

    }
}
