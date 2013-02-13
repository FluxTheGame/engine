using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    public class Wormhole : GameObject
    {

        public bool inward;
        

        public Wormhole(bool suck) : base()
        {
            model = ContentManager.Model("chicken");
            inward = suck;
            scale = 0.08f;
        }

        public override void Update()
        {
            if (inward) 
                GridManager.Pinch(position, 100, 0.005f);
            else
                GridManager.Bloat(position, 100, 0.005f);

            base.Update();
        }
    }
}
