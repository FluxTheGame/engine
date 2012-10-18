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


        public User(Texture2D ico)
        {
            position = Vector2.Zero;
            icon = ico;
        }



        public void Update()
        {


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
