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
        public static int highestPoints = 100;

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
                int display = (int)o["display"];
                users.Add(new User((string)o["username"], id, teamId, display));
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

            EventManager.On("user:newTeam", (o) =>
            {
                User user = UserByID((int)o["id"]);
                if (user != null)
                    user.collector = CollectorManager.CollectorByID((int)o["teamId"]);
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Dictionary<string, string> audioFiles = new Dictionary<string, string>();
            audioFiles.Add("user.spawn", "sfx/cursor/cursor_in.wav");
            audioFiles.Add("user.death", "sfx/cursor/cursor_out.wav");

            Audio.Load(audioFiles);
            
            base.LoadContent();
        }

        public User UserByID(int id)
        {
            return users.FirstOrDefault(u => u.id == id);
        }

        protected void FindHighestPoints()
        {
            UserManager.highestPoints = 100;
            foreach (User u in instance.users)
            {
                if (u.points > UserManager.highestPoints) UserManager.highestPoints = u.points;
            }
        }

        public static void Remove(User user)
        {
            instance.users.Remove(user);
        }

        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            FindHighestPoints();
            objects = users.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }
        
    }
}
