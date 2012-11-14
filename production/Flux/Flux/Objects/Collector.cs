using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    public class Collector : GridObject
    {

        protected int health = 100;
        protected int capacity = 100;
        protected int resources = 0;
        protected int currentSize = 1; //Temporary while using sprites

        private int normalCapacity;

        public Collector() : base()
        {
            icon = ContentManager.collectorSm;
            position = new Vector2(100, 100);
            normalCapacity = capacity;
        }

        public override void Update()
        {
            drag = capacity / normalCapacity;

            CollectResources();

            //Keep collector inside screen - For demo (Temporary)
            if (position.X > 680) position.X = 680;
            if (position.Y > 680) position.Y = 680;
            if (position.X < 20) position.X = 20;
            if (position.Y < 20) position.Y = 20;
            base.Update();
        }

        public int SuckRadius()
        {
            float percent = ((float)resources / (float)capacity);
            int radius = (int)(percent*10 + 20);
            if (radius > 100) radius = 100;
            return radius;
        }

        public void MergeWith(Collector other)
        {
            capacity += other.capacity;
            resources += other.resources;

            currentSize += other.currentSize; //Temporary while using sprites
            if (currentSize > 1) icon = ContentManager.collectorM;
            if (currentSize > 2) icon = ContentManager.collectorLg;

            health = 100;
        }

        public void Collect(Resource resource)
        {
            resources++;
        }

        private void CollectResources()
        {
            ResourceManager.Gather(this);
        }

    }
}
