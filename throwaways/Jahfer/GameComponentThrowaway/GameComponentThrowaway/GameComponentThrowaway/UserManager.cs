using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class UserManager : DrawableGameComponent {

        List<User> userList = new List<User>();

        public UserManager(Game g)
            : base(g) {
                g.Components.Add(this);
        }

        public override void Initialize() {
            Console.WriteLine("UserManager has been initialized!");
            base.Initialize();
        }

        protected override void LoadContent() {
            Console.WriteLine("UserManager has loaded its content!");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            Console.WriteLine("UserManager has been updated!");

            foreach (User user in userList) {
                user.update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            Console.WriteLine("UserManager has been drawn!");

            foreach (User user in userList) {
                user.draw();
            }

            base.Draw(gameTime);
        }

        public bool createNewUser() {
            this.userList.Add(new User());
            Console.WriteLine("UserManager has added a new User!");
            return true;
        }

    }
}
