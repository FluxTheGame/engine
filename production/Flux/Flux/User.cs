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

            //Perhaps send out an event for warping the grid...
            /*if (keyState.IsKeyDown(Keys.Down))
            {
                vf.addForceCircle(mousePos, 80.0f, 0.05f, true);
            } if (keyState.IsKeyDown(Keys.Up))
            {
                vf.addForceCircle(mousePos, 80.0f, 0.05f, false);
            }*/


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon, position, Color.White);
        }


    }
}
