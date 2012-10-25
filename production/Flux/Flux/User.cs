using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    class User
    {
        public string username;
        public int id;
        public Vector2 position;
        Texture2D icon;
        Vector2 delta;


        public User(Texture2D ico, string user, int identity)
        {
            position = new Vector2(350, 350);
            delta = Vector2.Zero;
            icon = ico;
            username = user;
            id = identity;
        }

        public void setDelta(int x, int y)
        {
            delta = new Vector2((float)x, (float)y);
            delta = Vector2.Divide(delta, 20);
        }

        public void Update()
        {

            position = Vector2.Add(position, delta);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon, position, Color.White);
        }


    }
}
