﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    class GridObject : GameObject
    {

        public GridObject(Texture2D ico)
            : base(ico)
        {

        }

        public override void Update()
        {
            Vector2 force = GridManager.GetForce(display, position, 100.0f);
            position = Vector2.Add(position, force);
            base.Update();
        }

    }
}
