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

        protected DateTime expiry;
        protected bool inward;


        public Wormhole(bool suck) : base()
        {
            model = ContentManager.Model("chicken");
            position = new Vector2(500, 300);
            expiry = created.AddSeconds(Randomizer.RandomInt(20, 30));
            inward = suck;
            scale = 0.08f;
        }

        public override void Update()
        {
            if (DateTime.Now.CompareTo(expiry) > 0)
                WormholeManager.Remove(this);

            if (inward) 
                GridManager.Pinch(position, 100, 0.015f);
            else
                GridManager.Bloat(position, 100, 0.015f);

            base.Update();
        }
    }
}
