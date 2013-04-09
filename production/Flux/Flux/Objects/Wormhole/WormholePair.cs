using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Flux
{
    public class WormholePair : GameObject
    {
        public Wormhole one;
        public Wormhole two;
        public bool dying = false;
        protected DateTime expiry;

        public WormholePair(bool oneInward, Vector2 position, int displayOne) : base()
        {
            one = new Wormhole(oneInward, position, displayOne);
            Audio.Play("wormhole.origin", one.display);

            two = new Wormhole(!oneInward, position, ScreenManager.Opposite(displayOne));
            Audio.Play("wormhole.opposite", two.display);

            float lifespan = Randomizer.RandomInt(10, 20);
            expiry = created.AddSeconds(lifespan);
        }

        public bool Make(Vector2 position, int display) {
            if (Vector2.Distance(ScreenManager.AdjustedPosition(one.position, one.display), ScreenManager.AdjustedPosition(position, display)) > 200 &&
                Vector2.Distance(ScreenManager.AdjustedPosition(two.position, two.display), ScreenManager.AdjustedPosition(position, display)) > 200)
            {
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
            if (DateTime.Now > expiry)
            {
                one.Collapse();
                two.Collapse();

                dying = true;

                if (DateTime.Now > expiry.AddSeconds(2))
                {
                    WormholeManager.Remove(this);
                }
            }

            one.Update();
            two.Update();
        }

        private void TriggerSuction(GameObject passenger, Wormhole endpoint)
        {
            float max = passenger.scale;
            passenger.disabled = true;
            Tweenerizer.Ease(EasingType.EaseIn, 0, 1, 300,
                // update with easing
                (ease, incr) => passenger.scale = max - (ease * max),
                // on complete
                () => {
                    // move to new display
                    Transport(passenger, endpoint);
                    // scale back up
                    Tweenerizer.Ease(EasingType.EaseOut, 0, 1, 300,
                        (ease, incr) => passenger.scale = (ease * max));
                    // it's aliiiiive!
                    passenger.disabled = false;
                });
        }

        public void Suck(GameObject passenger)
        {
            if (!dying)
            {
                if (one.inward && GameObject.Distance(one, passenger) < 20f)
                {
                    TriggerSuction(passenger, two);
                }

                else if (two.inward && GameObject.Distance(two, passenger) < 20f)
                {
                    TriggerSuction(passenger, one);
                }
            }
        }

        private void Transport(GameObject passenger, Wormhole destination)
        {
            passenger.display = destination.display;
        }
    }
}
