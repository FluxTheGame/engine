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

namespace VectorField
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        MouseState mouseState;
        SpriteBatch spriteBatch;
        VectorField vf;

        Vector2 mousePos;
        Vector2 objectPos;

        Texture2D icon;
        Texture2D mouseIcon;
        Texture2D objectIcon;

        SpriteFont font;



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
                50, 50
            );

            objectPos = new Vector2(
                GraphicsDevice.Viewport.Width / 2, 
                GraphicsDevice.Viewport.Height / 2
            );

            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            icon = Content.Load<Texture2D>("box");
            mouseIcon = Content.Load<Texture2D>("mouse");
            objectIcon = Content.Load<Texture2D>("object");

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

            Vector2 force = vf.getForceAtPosition(objectPos);
            objectPos = Vector2.Add(objectPos, Vector2.Divide(force, 5.0f));

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            vf.Draw(spriteBatch, font);

            spriteBatch.Draw(mouseIcon, mousePos, Color.White);
            spriteBatch.Draw(objectIcon, objectPos, Color.White);

            Vector2 force = vf.getForceAtPosition(mousePos);
            string output = Math.Round(force.X).ToString() + " " + Math.Round(force.Y).ToString();
            Vector2 outputSize = font.MeasureString(output);
            Vector2 fontPos = new Vector2(GraphicsDevice.Viewport.Width - 10 - outputSize.X, GraphicsDevice.Viewport.Height - outputSize.Y);
            spriteBatch.DrawString(font, output, fontPos, Color.CornflowerBlue);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
