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
        protected int resources = 50;
        protected int normalCapacity;
        public int attackRadius = 450;
        public int id;


        public Collector(int idNumber): base()
        {
            id = idNumber;
            model = ContentManager.Model("collector");
            position = new Vector2(100*id, 100*id);
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
            o.Add("id", id);
            EventManager.Emit("collector:burst", o);
            Die();
        }

        public void Die()
        {
            CollectorManager.Remove(this);
        }

        public int SuckRadius()
        {
            int radius = (int)(capacity*0.5f + resources);
            if (radius > 100) radius = 100;
            return radius;
        }

        public void MergeWith(Collector other)
        {
            capacity += other.capacity;
            resources += other.resources;

            health = 100;
        }

        public void BeAttacked(int damage)
        {
            health -= damage;
        }

        public void Attack()
        {
            if (resources > 0) {
                resources--;
                EnemyManager.AttackClosestEnemy(this);
            }
        }

        public void Collect(Resource resource)
        {
            resources++;
            scale = ((float)capacity * 0.01f + (float)resources * 0.01f) * 0.1f;
        }

        private void CollectResources()
        {
            ResourceManager.Gather(this);
        }
    }
}
