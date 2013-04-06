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
        private float spin;
        private Random random = new Random();
        private float xOffset;

        public Leaf(Vector3 location, string modelName, Vector3 parentLocation, float speed = 0.05f) : base(location, modelName, speed)
        {
            sinAngle = 0f;
            intensity = 0f;
            spin = 0f;
            xOffset = (float)random.Next(0, 20);

            //X is up
            Vector3 treeCentre = parentLocation + new Vector3(0, 2, 0);
            Vector3 midToLeaf = (location - treeCentre);
            float leafTilt = (float)Math.Atan(midToLeaf.Y / midToLeaf.X);
            //Vector3 starBurt = Vector3.Dot();

            rotation = new Vector3(0f, 0f, (float)(leafTilt / (2 * Math.PI)) * 180);

            //debuf redundancy
            rotation = rotation;
        }

        protected override void MoveToCollector()
        {

            Vector2 difference = collector.position - position;
            double rad = Math.Atan2((difference.Y), (difference.X));
            float offset = MathHelper.ToDegrees((float)rad);
                        
            intensity += 0.14f;

            if (intensity > 30)//if the resource is in transit to the collector
            {
                base.MoveToCollector();

                spin += 15f;
                rotation = new Vector3(xOffset, 0f, spin);
            }
            else if (collector.isDying)//if the collector ceases to exist
            {
                intensity -= 0.25f;
                if (intensity < 0) collector = null;                
            }
            else//if the resources are in range to be collected
            {
                sinAngle += 5f + intensity;
                rotation = new Vector3(10 * ((float)Math.Sin((double)(Math.PI * (sinAngle + 30) / 180))) , 0f , offset + (45 * ((float)Math.Sin((double)(Math.PI * sinAngle / 180)))));

            }
        }
    }
}
