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

        private float sinAngle;
        private float intensity;

        public Leaf(Vector3 location, string modelName, float speed = 0.05f) : base(location, modelName, speed)
        {
            sinAngle = 0f;
            intensity = 0f;
        }

        protected override void MoveToCollector()
        {
            sinAngle += 5f + intensity;
            rotation = new Vector3(10 * ((float)Math.Sin((double)(Math.PI * (sinAngle + 30) / 180))), 0f, 45 * ((float)Math.Sin((double)(Math.PI * sinAngle / 180))));

            intensity += 0.14f;

            if (intensity > 30)
            {
                base.MoveToCollector();
            }
            //Override to shake or something
            //You don't have to call base.Update.. it just makes the resource move towards the collector without restraint
            //base.MoveToCollector();
        }
    }
}
