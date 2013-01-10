using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class GameObject
    {

        public Vector2 position;
        public int display;
        public Model model;
        protected DateTime created;

        public GameObject()
        {
            position = new Vector2(350, 350);
            display = 0;
            created = DateTime.Now;
        }

        public virtual void Update()
        {
        }

        public virtual void Draw()
        {
            
        }

    }
}
