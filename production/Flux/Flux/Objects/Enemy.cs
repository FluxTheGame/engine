using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    class Enemy : GridObject
    {

        public Enemy() : base()
        {
            icon = ContentManager.enemy;
        }

        public override void Update()
        {
            GridManager.Bloat(display, position, 40.0f, 0.05f);
            base.Update();
        }

    }
}
