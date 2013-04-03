﻿using System;
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
        protected int capacity = 200;
        protected int collected = 0;
        protected int normalCapacity;
        protected Schedualizer heartbeatSchedule;
        protected float scaleRate = 0.0008f;
        protected float targetScale;
        protected int spawnBuffer = 150;
        protected AnimSet collectorAnim;

        public int attackRadius = 500;
        public int collectRadius = 100;
        public int id;
        public bool isDying = false;
        public int numCollectors = 1;
        public Color teamColour;

        private List<User> users;
        private List<Projectile> projectiles;


        public Collector(int idNumber): base()
        {
            id = idNumber;

            position = new Vector2(
                Randomizer.RandomInt(spawnBuffer, (int)ScreenManager.window.X - spawnBuffer), 
                Randomizer.RandomInt(spawnBuffer, (int)ScreenManager.window.Y - spawnBuffer)
            );
            
            display = Randomizer.RandomDisplay();
            normalCapacity = capacity;
            dampening = 0.9f;
            heartbeatSchedule = new Schedualizer(0f, 5f, 5f);
            targetScale = scale;
            teamColour = TeamColour.Get();

            SetupAnimations();
            SendHeartbeat();

            Audio.Play("collector.spawn", display);

            users = new List<User>();
            projectiles = new List<Projectile>();
        }

        public override void Update()
        {
            drag = capacity / normalCapacity;

            CollectResources();
            UpdateProjectiles();
            collectorAnim.Update(position);

            if (collected >= capacity)
                Burst(true);

            if (heartbeatSchedule.IsOn())
            {
                SendHeartbeat();
            }

            if (scale < targetScale)
            {
                scale += scaleRate * 0.1f;
            }
            
            base.Update();
        }

        protected void SendHeartbeat()
        {
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", id);
            o.Add("health", health);
            o.Add("capacity", capacity);
            o.Add("fill", collected);
            o.Add("colour", TeamColour.ToHex(teamColour, true));
            EventManager.Emit("collector:heartbeat", o);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(gameTime);
            }

            ScreenManager.SetTarget(display);
            ScreenManager.spriteBatch.Begin();
            collectorAnim.Draw();
            ScreenManager.spriteBatch.End();
        }

        public void Burst(bool completed)
        {
            OrderedDictionary o = new OrderedDictionary();
            o.Add("id", id);

            if (completed)
            {
                o.Add("points", collected);
                Audio.Play("collector.complete", display);
            }
            else
            {
                // only give a percentage of points
                o.Add("points", collected * (collected / capacity));
                Audio.Play("collector.death", display);
            }

            o.Add("completed", (completed) ? 1 : 0);
            EventManager.Emit("collector:burst", o);

            Die();
        }

        public void Die()
        {
            Console.WriteLine("Dying...");
            foreach (User user in users)
            {
                user.collector = null;
            }
            isDying = true;

            TeamColour.Put(teamColour);
            CollectorManager.Remove(this);
        }

        public void MergeWith(Collector other)
        {
            Audio.Play("collector.merge", display);

            OrderedDictionary o = new OrderedDictionary();
            o.Add("team_1", id);
            o.Add("team_2", other.id);
            EventManager.Emit("collector:merge", o);

            capacity += other.capacity;
            collected += other.collected;
            targetScale += scaleRate * (other.collected * 0.5f);
            numCollectors += other.numCollectors;

            foreach (User u in other.users)
            {
                u.collector = this;
                users.Add(u);
            }

            health = 100;
            CollectorManager.Remove(other);
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
                Burst(false);
            }
        }

        public void Attack()
        {
            if (collected > 0) {
                Enemy enemy = EnemyManager.InRange(this);
                if (enemy != null) {
                    projectiles.Add(new Projectile(enemy, this));
                    Audio.Play("collector.weapon", display);
                    collected--;
                    Console.WriteLine("Attacking...");
                }
            }
        }

        public void Collect()
        {
            if (!isDying)
            {
                //Audio.Play("collector.resource" + ((collected % 4) + 1), display);
                collected++;
                targetScale += scaleRate;
            }
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

        public void SetupAnimations()
        {
            collectorAnim = new AnimSet(
                new Spritesheet[] {
                    new Spritesheet("collector_intro", 52, new Point(350, 600), 1),
                    new Spritesheet("collector_static", 3, new Point(350, 600), -1, 0, false)
                }
            );
        }
    }
}
