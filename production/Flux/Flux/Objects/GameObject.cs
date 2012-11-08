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
        public Texture2D icon;


        public GameObject()
        {
            position = new Vector2(350, 350);
            display = 0;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (icon != null)
            {
                spriteBatch.Draw(icon, position, Color.White);
            }
        }

    }
}
