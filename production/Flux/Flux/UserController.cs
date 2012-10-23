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
        Dictionary<int, User> users;

        
        public UserController(Game game)
            : base(game)
        {
        }


        public override void Initialize()
        {
            users = new Dictionary<int, User>();

            EventManager.On("user:join", (o) =>
            {
                int id = (int)o["id"];
                users.Add(id, new User(icon, (string)o["username"], id));
            });

            EventManager.On("user:touch", (o) =>
            {
                int id = (int)o["id"];
                users[id].setDelta((int)o["x"], (int)o["y"]);
            });

            EventManager.On("user:touchEnd", (o) =>
            {
                int id = (int)o["id"];
                users[id].setDelta(0, 0);
            });

            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {

            foreach (KeyValuePair<int, User> pair in users)
            {
                users[pair.Key].Update();
            }

            base.Update(gameTime);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            icon = Game.Content.Load<Texture2D>("mouse");

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (KeyValuePair<int, User> pair in users)
            {
                users[pair.Key].Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
