using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    public class User : GameObject
    {
        public string username;
        public int id;

        private Vector2 delta;


        public User(string user, int idNumber) : base ()
        {
            delta = Vector2.Zero;
            username = user;
            id = idNumber;
            sprite = ContentManager.user;
        }

        public void SetDelta(int x, int y)
        {
            delta = new Vector2((float)x, (float)y);
            delta = Vector2.Divide(delta, 20);
        }

        public override void Update()
        {
            position = Vector2.Add(position, delta);
            base.Update();
        }

        public override void Draw()
        {
            ScreenManager.spriteBatch.Begin();
            ScreenManager.spriteBatch.DrawString(ContentManager.userfont, username, position, Color.White);
            ScreenManager.spriteBatch.End();
            base.Draw();
        }

    }
}
