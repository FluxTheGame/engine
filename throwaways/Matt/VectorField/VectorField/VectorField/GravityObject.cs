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
   
    public class GravityObject : Microsoft.Xna.Framework.DrawableGameComponent
    {

        Vector2 position;
        Texture2D icon;

        public GravityObject(Game game)
            : base(game)
        {
            
        }

       
        public override void Initialize()
        {
            position = new Vector2(100, 100);

            base.Initialize();
        }

        
       
        public override void Update(GameTime gameTime)
        {
           

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
