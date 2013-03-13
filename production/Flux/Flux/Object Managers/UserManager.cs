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

            EventManager.On("user:new", (o) =>
            {
                int id = (int)o["id"];
                int teamId = (int)o["teamId"];
                users.Add(new User((string)o["username"], id, teamId));
            });

            EventManager.On("user:disconnect", (o) =>
            {
                int id = (int)o["id"];
                var user = users.FirstOrDefault(u => u.id == id);

                if (user != null) user.Disconnect();
            });

            EventManager.On("user:getPoints", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.GetPoints((int)o["value"]);
            });

            EventManager.On("user:getBadge", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.GetBadge((string)o["type"]);
            });

            EventManager.On("user:touch", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.SetDelta((int)o["x"], (int)o["y"]);
            });

            EventManager.On("user:touchEnd", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.SetDelta(0, 0);
            });

            EventManager.On("user:bloat", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.BloatStart();
            });

            EventManager.On("user:pinch", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.PinchStart();
            });

            EventManager.On("user:bloatEnd", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.BloatEnd();
            });

            EventManager.On("user:pinchEnd", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null) user.PinchEnd();
            });

            base.Initialize();
        }

        public User UserByID(int id)
        {
            return users.FirstOrDefault(u => u.id == id);
        }

        public static void Remove(User user)
        {
            instance.users.Remove(user);
        }

        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = users.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }
        
    }
}
