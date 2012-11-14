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
    
    public class UserManager : Manager
    {

        public static UserManager instance;
        List<User> users;

        
        public UserManager(Game game) : base(game)
        {
            UserManager.instance = this;
        }


        public override void Initialize()
        {
            users = new List<User>();

            EventManager.On("user:join", (o) =>
            {
                int id = (int)o["id"];
                users.Add(new User((string)o["username"], id));
            });

            EventManager.On("user:touch", (o) =>
            {
                User user = UserByID((int)o["id"]);
                user.SetDelta((int)o["x"], (int)o["y"]);
            });

            EventManager.On("user:touchEnd", (o) =>
            {
                User user = UserByID((int)o["id"]);
                user.SetDelta(0, 0);
            });

            EventManager.On("user:bloat", (o) =>
            {
                User user = UserByID((int)o["id"]);
                GridManager.Bloat(0, user.position, 60.0f, 0.05f);
            });

            EventManager.On("user:pinch", (o) =>
            {
                User user = UserByID((int)o["id"]);
                GridManager.Pinch(0, user.position, 60.0f, 0.05f);
            });

            base.Initialize();
        }


        public User UserByID(int id)
        {
            return users.FirstOrDefault(u => u.id == id);
        }


        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = users.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }
        
    }
}
