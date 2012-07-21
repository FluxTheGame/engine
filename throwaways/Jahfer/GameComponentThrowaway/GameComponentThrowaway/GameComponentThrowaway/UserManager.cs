using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class UserManager{

        List<User> userList = new List<User>();
        Game game;

        public UserManager(Game g) {
            game = g;
        }

        public void Initialize() {
            //Console.WriteLine("UserManager has been initialized!");
        }

        protected void LoadContent() {
            //Console.WriteLine("UserManager has loaded its content!");
        }

        public void Update(GameTime gameTime) {
            //Console.WriteLine("UserManager has been updated!");

            foreach (User user in userList) {
                user.update();
            }

        }

        public void Draw(GameTime gameTime) {
            //Console.WriteLine("UserManager has been drawn!");
            foreach (User user in userList) {
                user.draw();
            }
        }

        public bool createNewUser() {
            this.userList.Add(new User(game.Content));
            //Console.WriteLine("UserManager has added a new User!");
            return true;
        }

    }
}
