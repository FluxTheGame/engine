using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    public class GridObject : GameObject
    {

        protected float drag = 1.0f;

        public GridObject() : base()
        {
            dampening = 0.8f;
        }

        public override void Update()
        {
            Vector2 force = GridManager.GetForce(position, 100.0f);
            force = Vector2.Divide(force, drag);
            velocity = Vector2.Add(velocity, force);

            CheckWormholes();

            base.Update();
        }

        protected void CheckWormholes()
        {
            WormholeManager.Suck(this);
        }
    }
}
