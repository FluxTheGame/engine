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


namespace Flux
{
    
    public class GridObjectManager : DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        Texture2D icon;
        List<GridObject> objects;
        VectorField vf;

        
        public GridObjectManager(Game game, VectorField vectorField)
            : base(game)
        {
            vf = vectorField;
        }


        public override void Initialize()
        {
            objects = new List<GridObject>();
            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {

            foreach (GridObject go in objects)
            {
                go.Update();
            }

            base.Update(gameTime);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            icon = Game.Content.Load<Texture2D>("object");

            for (int i = 0; i < 3; i++)
            {
                objects.Add(
                    new GridObject(icon,
                        new Vector2(
                            GraphicsDevice.Viewport.Width / 2,
                            GraphicsDevice.Viewport.Height / 2), vf));
            }

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (GridObject go in objects)
            {
                go.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
