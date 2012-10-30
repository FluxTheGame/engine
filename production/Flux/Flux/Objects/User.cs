using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    class User : GameObject
    {
        public string username;
        public int id;
        Vector2 delta;


        public User(Texture2D ico, string user, int identity) : base (ico)
        {
            delta = Vector2.Zero;
            username = user;
            id = identity;
        }

        public void SetDelta(int x, int y)
        {
            delta = new Vector2((float)x, (float)y);
            delta = Vector2.Divide(delta, 20);
        }

        public void Update()
        {
            position = Vector2.Add(position, delta);
        }


    }
}
