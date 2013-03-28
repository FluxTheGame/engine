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
    public class Water : Resource
    {

        protected float minOpacity = 0.2f;
        protected float maxOpacity = 1f;


        public Water(Vector3 location, string modelName, float speed = 0.05f) : base(location, modelName, speed)
        {
            opacity = minOpacity;
            modelScale = 0.05f;
        }

        protected override void MoveToCollector()
        {
            Vector2 difference = position - collector.position;
            double radians = Math.Atan2((difference.Y), (difference.X));
            rotation = new Vector3((float)MathHelper.ToDegrees((float)radians), 0, 0);

            base.MoveToCollector();
        }

        public override void AssignCollector(Collector collector)
        {
            Tweenerizer.Ease(EasingType.EaseInOut, minOpacity, maxOpacity, 100, (ease, incr) =>
            {
                opacity = ease;
            });

            base.AssignCollector(collector);
        }
    }
}
