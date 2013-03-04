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

        public int attackRadius = 300;
        public int id;

        private List<User> users;
        private List<Projectile> projectiles;


        public Collector(int idNumber): base()
        {
            id = idNumber;
            model = ContentManager.Model("collector");
            position = new Vector2(200*id, 200*id);
            normalCapacity = capacity;
            users = new List<User>();
            projectiles = new List<Projectile>();
        }

        public override void Update()
        {
            drag = capacity / normalCapacity;

            CollectResources();
            UpdateProjectiles();

            if (resources >= capacity)
                Burst();

            if (health <= 0)
                Die();
            
            base.Update();
        }

        public override void Draw()
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw();
            }
            base.Draw();
        }

        public void Burst()
        {
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", id);
            o.Add("points", resources);
            EventManager.Emit("collector:burst", o);
            Die();
        }

        public void Die()
        {
            CollectorManager.Remove(this);
        }

        public float SuckRadius()
        {
            float radius = (capacity*0.05f + resources);
            if (radius > 3) radius = 3f;
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
            foreach (User user in users)
            {
                user.Alert();
            }
        }

        public void Attack()
        {
            if (resources > 0) {
                Enemy enemy = EnemyManager.ClosestEnemy(this);
                if (GameObject.Distance(this, enemy) <= this.attackRadius) {
                    projectiles.Add(new Projectile(enemy, this));
                    resources--;
                }
            }
        }

        public void Collect(Resource resource)
        {
            resources++;
            scale = ((float)capacity * 0.01f + (float)resources * 0.01f) * 0.1f;
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }

        private void CollectResources()
        {
            ResourceManager.Gather(this);
        }

        private void UpdateProjectiles()
        {
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].Update();
            }
        }

        public void DestroyProjectile(Projectile projectile)
        {
            projectiles.Remove(projectile);
        }
    }
}
