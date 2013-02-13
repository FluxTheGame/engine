using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class WormholePair : GameObject
    {
        public Wormhole one;
        public Wormhole two;
        protected DateTime expiry;

        public WormholePair(bool oneInward, Vector2 onePosition) : base()
        {
            one = new Wormhole(oneInward);
            one.position = onePosition;

            two = new Wormhole(!oneInward);
            two.position = new Vector2(onePosition.X + 300, onePosition.Y);

            expiry = created.AddSeconds(Randomizer.RandomInt(20, 30));
        }

        public bool Make(Vector2 position) {
            if (Vector2.Distance(one.position, position) > 200 &&
                Vector2.Distance(two.position, position) > 200) {
                return true;
            }
            return false;
        }

        public override void Draw()
        {
            one.Draw();
            two.Draw();
        }

        public override void Update()
        {
            if (DateTime.Now.CompareTo(expiry) > 0)
            {
                WormholeManager.Remove(this);
            }

            one.Update();
            two.Update();
        }

        public void Suck(GameObject passenger)
        {
            if (one.inward && GameObject.Distance(one, passenger) < 20f)
                Transport(passenger, two);

            else if (two.inward && GameObject.Distance(two, passenger) < 20f)
                Transport(passenger, one);
        }

        private void Transport(GameObject passenger, Wormhole destination)
        {
            passenger.position = destination.position;
        }
    }
}
