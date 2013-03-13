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
        protected Schedualizer heartbeatSchedule;

        public int attackRadius = 500;
        public int id;
        public bool isDying = false;

        private List<User> users;
        private List<Projectile> projectiles;


        public Collector(int idNumber): base()
        {
            id = idNumber;
            model = ContentManager.Model("collector");
            position = new Vector2(70*id + 100, 70*id + 100);
            normalCapacity = capacity;
            dampening = 0.98f;
            heartbeatSchedule = new Schedualizer(0f, 5f, 5f);

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

            if (heartbeatSchedule.IsOn())
            {
                OrderedDictionary o = new OrderedDictionary();
                o.Add("id", id);
                o.Add("health", health);
                o.Add("capacity", capacity);
                o.Add("fill", resources);
                EventManager.Emit("collector:heartbeat", o);
            }
            
            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(gameTime);
            }
            base.Draw(gameTime);
        }

        public void Burst()
        {
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", id);
            o.Add("points", resources);
            EventManager.Emit("collector:burst", o);
            Console.WriteLine("Collector " + id + ":  FULL");
            Die();
        }

        public void Die()
        {
            foreach (User user in users)
            {
                user.collector = null;
            }
            isDying = true;
            CollectorManager.Remove(this);
        }

        public float SuckRadius()
        {
            float radius = (capacity + resources)*0.015f;
            if (radius > 3) radius = 3f;
            return radius;
        }

        public void MergeWith(Collector other)
        {
            OrderedDictionary o = new OrderedDictionary();
            o.Add("team_1", id);
            o.Add("team_2", other.id);
            EventManager.Emit("collector:merge", o);

            capacity += other.capacity;
            resources += other.resources;

            foreach (User u in other.users)
            {
                u.collector = this;
                users.Add(u);
            }

            health = 100;
        }

        public void BeAttacked(int damage)
        {
            health -= damage;
            foreach (User user in users)
            {
                user.Alert();
            }
            Console.WriteLine("Collector " + id + ":  Got Attacked, health: " + health);
            if (health <= 0)
            {
                Console.WriteLine("Collector " + id + ":  DEATH");
                Die();
            }
        }

        public void Attack()
        {
            if (resources > 0) {
                Enemy enemy = EnemyManager.ClosestEnemy(this);
                if (GameObject.Distance(this, enemy) <= attackRadius) {
                    projectiles.Add(new Projectile(enemy, this));
                    resources--;
                }
            }
        }

        public void Collect(Resource resource)
        {
            if (!isDying)
            {
                resources++;
                scale += 0.0008f;
            }
            resource.Gather();
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
