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
    
    public class UserController : DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        Texture2D icon;
        List<User> users;

        
        public UserController(Game game)
            : base(game)
        {
        }


        public override void Initialize()
        {
            users = new List<User>();
            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {

            foreach (User user in users)
            {
                user.Update();
            }

            base.Update(gameTime);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            icon = Game.Content.Load<Texture2D>("mouse");
            users.Add(new User(icon));

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (User user in users)
            {
                user.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}
