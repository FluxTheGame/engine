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

        public WormholePair(bool oneInward, Vector2 position, int displayOne) : base()
        {
            one = new Wormhole(oneInward);
            one.position = position;
            one.display = displayOne;
            Audio.Play("wormhole.origin", one.display);

            two = new Wormhole(!oneInward);
            two.position = position;
            two.display = ScreenManager.Opposite(displayOne);
            Audio.Play("wormhole.opposite", two.display);

            expiry = created.AddSeconds(Randomizer.RandomInt(20, 30));
        }

        public bool Make(Vector2 position) {
            if (Vector2.Distance(one.position, position) > 200 &&
                Vector2.Distance(two.position, position) > 200) {
                return true;
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            one.Draw(gameTime);
            two.Draw(gameTime);
        }

        public override void Update()
        {
            if (DateTime.Now.CompareTo(expiry) > 0)
            {
                WormholeManager.Remove(this);
            }

            ScreenManager.SetTarget(one.display);
            one.Update();
            ScreenManager.SetTarget(two.display);
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
            passenger.display = destination.display;
        }
    }
}
