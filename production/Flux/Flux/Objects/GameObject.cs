using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    class GameObject
    {

        public Vector2 position;
        public int display;
        protected Texture2D icon;


        public GameObject(Texture2D ico)
        {
            position = new Vector2(350, 350);
            icon = ico;
            display = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon, position, Color.White);
        }

    }
}
