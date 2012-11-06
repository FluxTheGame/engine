using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    class Collector : GridObject
    {

        public int capacity = 10;
        public int resources = 0;

        public Collector(Texture2D ico)
            : base(ico)
        {
            position = new Vector2(100, 100);
        }

        public override void Update()
        {
            if (position.X > 680) position.X = 680;
            if (position.Y > 680) position.Y = 680;
            if (position.X < 20) position.X = 20;
            if (position.Y < 20) position.Y = 20;
            base.Update();
        }

    }
}
