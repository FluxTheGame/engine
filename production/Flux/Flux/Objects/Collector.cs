using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
        protected int normalCapacity;


        public Collector() : base()
        {
            model = ContentManager.collector;
            position = new Vector2(100, 100);
            normalCapacity = capacity;
        }

        public override void Update()
        {
            drag = capacity / normalCapacity;

            CollectResources();

            if (resources >= capacity)
                Burst();

            if (health <= 0)
                Die();
            
            base.Update();
        }

        public void Burst()
        {
            OrderedDictionary o = new OrderedDictionary();
            o.Add("hello", "world");
            EventManager.Emit("collector:burst", o);
            Die();
        }

        public void Die()
        {
            Console.WriteLine("Collector Die...");
            CollectorManager.Remove(this);
        }

        public int SuckRadius()
        {
            float percent = ((float)resources / (float)capacity);
            int radius = (int)(percent*10 + 50);
            if (radius > 100) radius = 100;
            return radius;
        }

        public void MergeWith(Collector other)
        {
            capacity += other.capacity;
            resources += other.resources;

            health = 100;
        }

        public void Collect(Resource resource)
        {
            resources++;
            scale = ((float)resources / (float)capacity * 0.5f) + 0.1f;
        }

        private void CollectResources()
        {
            ResourceManager.Gather(this);
        }

    }
}
