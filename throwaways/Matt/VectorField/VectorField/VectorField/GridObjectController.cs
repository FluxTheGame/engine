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
    
    public class GridObjectController : DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        Texture2D objectIcon;
        List<GridObject> objects;

        
        public GridObjectController(Game game)
            : base(game)
        {   
        }


        public override void Initialize()
        {
            objects = new List<GridObject>();
            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            objectIcon = Game.Content.Load<Texture2D>("object");

            objects.Add(
                new GridObject(objectIcon, 
                    new Vector2(
                        GraphicsDevice.Viewport.Width / 2, 
                        GraphicsDevice.Viewport.Height / 2)));

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
