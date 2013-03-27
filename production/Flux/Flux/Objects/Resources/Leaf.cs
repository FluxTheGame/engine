using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;



namespace Flux
{
    public class Leaf : Resource
    {

        public Leaf(Vector3 location, string modelName, float speed = 0.05f) : base(location, modelName, speed)
        {
        }

        public override void Update()
        {
            Vector3 offsetFromTarget = Vector3.Zero;

            if (active)
            {
                // easing
                if (scale < 0.1f) scale += (0.11f - scale) * 0.1f;
                else if (collector != null)
                {
                    if (CollectorDistance() < 100f)
                        offsetFromTarget = GetIntensity(collector.Location(), location);
                    else
                        offsetFromTarget = GetIntensity(origLocation, location);

                    if (collector.isDying)
                        collector = null;
                }
                else
                {
                    offsetFromTarget = GetIntensity(origLocation, location);
                }

                location += offsetFromTarget * speed;

            }
            if (respawnDelay.IsOn())
            {
                active = true;
            }

            base.Update();
        }
    }
}
