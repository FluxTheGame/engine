
//#define PRODUCTION

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
        Texture2D skybox;

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
        SpriteBatch spriteBatch;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 800;

            #if PRODUCTION
                graphics.PreferredBackBufferWidth = 1280*4;
            
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

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            ground = new Ground();

            // Load audio
            Audio.MultiSpeakerOutput_Load();

            Dictionary<string, string> audioFiles = new Dictionary<string, string>();
            audioFiles.Add("ambient.flux", "sfx/ambient/flux_v2.wav");
            Audio.Load(audioFiles);

            Audio.Play("ambient.flux", 0, 0.3f, true);
            Audio.Play("ambient.flux", 1, 0.3f, true);
            Audio.Play("ambient.flux", 2, 0.3f, true);
            Audio.Play("ambient.flux", 3, 0.3f, true);

            //skybox = Content.Load<Texture2D>(@"images/skybox");

            /* For Testing */
            
            //Add Collector
            OrderedDictionary c = new OrderedDictionary();
            c.Add("id", 0);
            EventManager.Emit("collector:new", c);

            c["id"] = 1;
            EventManager.Emit("collector:new", c);

            /*c["id"] = 2;
            EventManager.Emit("collector:new", c);

            c["id"] = 3;
            EventManager.Emit("collector:new", c);*/

            //Add User
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", 99);
            o.Add("teamId", 0);
            o.Add("username", "DILBERT");
            o.Add("display", 0);
            EventManager.Emit("user:new", o);
            
            /* End for testing */
        }

      
        protected override void UnloadContent()
        {
            Audio.Dispose();
        }

       
        protected override void Update(GameTime gameTime)
        {

            //if (gameTime.IsRunningSlowly) Console.WriteLine("SLOWLY");
            
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            KeyboardState keyState = Keyboard.GetState();
            MouseState mouseState = Mouse.GetState();
            GridManager.Update();
            Tweenerizer.Update();
            Audio.Update();

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
                    //o.Add("type", "join");
                    //o.Add("value", 100);
                    //EventManager.Emit("user:getBadge", o);
                    //EventManager.Emit("user:getPoints", o);
                    //EventManager.Emit("user:disconnect", o);

                    OrderedDictionary c = new OrderedDictionary();
                    c.Add("id", 0);
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
            Light.Update(gameTime);

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

            //Draw Ground Plane
            ground.Draw();

            base.Draw(gameTime);

            GridManager.Draw();

            //Draw Grid

            ScreenManager.graphics.SetRenderTarget(null);
            ScreenManager.graphics.Clear(Color.SkyBlue);
            //ScreenManager.spriteBatch.Begin();

            #if PRODUCTION

                //ScreenManager.spriteBatch.Begin();
                for (int i = 0; i < 4; i++)
                {
                    RenderTarget2D tex = Shaderizer._drawShader(ScreenManager.Target(i), Shaderizer.tmpTarget, beautyPass, false);

                    ScreenManager.graphics.SetRenderTarget(null);
                    ScreenManager.spriteBatch.Draw(
                        tex, new Vector2(0, 0), 
                        ScreenManager.Target(i).Bounds, Color.White);
                    ScreenManager.spriteBatch.End();
                }


            /*float scale = 1f;
            int frameWidth = (int)(ScreenManager.window.X);

            for (int i = 0; i < 4; i++)
            {
                ScreenManager.spriteBatch.Draw(
                    (Texture2D)ScreenManager.Target(i), new Vector2(frameWidth * i, 0), 
                    ScreenManager.Target(i).Bounds, Color.White,
                    0, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            }*/
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
