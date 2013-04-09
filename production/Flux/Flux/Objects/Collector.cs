using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Timers;


namespace Flux
{
    public class Collector : GridObject
    {
        protected int health = 100;
        protected int damage = 0;
        protected int capacity = 300;
        protected int collected = 0;
        protected int normalCapacity;
        protected Schedualizer heartbeatSchedule;
        protected float scaleRate = 0.0005f;
        protected float targetScale;
        protected int spawnBuffer = 150;
        protected bool isPlayingAudio = false;

        protected Timer audioTimeout = new Timer(250);

        protected AnimSet collectorAnim;
        protected AnimSprite portalAnim;
        protected AnimSprite poofAnim;
        protected AnimSprite explodeAnim;

        public int attackRadius = 500;
        public int collectRadius = 100;
        public int id;
        public bool isDying = false;
        public int numCollectors = 1;
        public Color teamColour;

        private List<User> users;
        private List<Projectile> projectiles;

        enum States { Intro, Static, Outro1, Outro2, Outro3, Exploding };


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
            scale = 0.3f;
            targetScale = scale;
            teamColour = TeamColour.Get();

            SetupAnimations();
            SendHeartbeat();

            audioTimeout.Elapsed += new ElapsedEventHandler(audioTimeout_Elapsed);

            Audio.Play("collector.spawn", display);

            users = new List<User>();
            projectiles = new List<Projectile>();
        }

        private void audioTimeout_Elapsed(object sender, ElapsedEventArgs e)
        {
            isPlayingAudio = false;
        }

        public override void Update()
        {
            if (disabled) return;

            drag = capacity / normalCapacity;

            CollectResources();
            UpdateProjectiles();

            collectorAnim.Update(position);
            portalAnim.Update(position);
            poofAnim.Update(position);
            explodeAnim.Update(position);

            if (collected >= capacity)
                Burst(true);

            if (heartbeatSchedule.IsOn())
            {
                SendHeartbeat();
            }

            if (scale < targetScale)
            {
                scale += scaleRate;
            }

            if (!isDying && collectorAnim.sheet == (int)States.Static)
            {
                if (health <= 40) damage = 2;
                else if (health <= 70) damage = 1;
                else damage = 0;
                collectorAnim.SetFrame(damage);
                base.Update();
            }
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

            portalAnim.Draw(Color.White, scale * 0.7f);
            if (explodeAnim.playing) explodeAnim.Draw(Color.White, scale * 1.5f);
            collectorAnim.Draw(teamColour, scale);
            if (poofAnim.playing) poofAnim.Draw();

            ScreenManager.spriteBatch.End();
        }

        public void Burst(bool completed)
        {
            if (!isDying)
            {
                OrderedDictionary o = new OrderedDictionary();
                o.Add("id", id);

                if (completed)
                {
                    o.Add("points", collected);
                    Audio.Play("collector.complete", display);
                    PortalDie();
                }
                else
                {
                    // only give a percentage of points
                    o.Add("points", collected * (collected / capacity));
                    Audio.Play("collector.death", display);
                    ExplodeDie();
                }

                o.Add("completed", (completed) ? 1 : 0);
                EventManager.Emit("collector:burst", o);
            }
        }

        public void ExplodeDie() 
        {
            if (!isDying)
            {
                Die();

                explodeAnim.WhenFinished(() => {
                    TeamColour.Put(teamColour);
                    CollectorManager.Remove(this);
                });

                collectorAnim.Play((int)States.Exploding);
                collectorAnim.frameOffset = new Vector2(50, 137);
                explodeAnim.Play(0);
            }
        }

        public void PortalDie()
        {
            if (!isDying)
            {
                Die();

                portalAnim.WhenFinished(() => {
                    TeamColour.Put(teamColour);
                    CollectorManager.Remove(this);
                });

                collectorAnim.Play((int)States.Outro1 + damage);
                portalAnim.Play(0);
            }
        }

        protected void Die()
        {
            Console.WriteLine("Dying...");
            foreach (User user in users)
            {
                user.collector = null;
            }
            isDying = true;
        }

        public void MergeWith(Collector other)
        {
            poofAnim.Play(0);
            Audio.Play("collector.merge", display);

            OrderedDictionary o = new OrderedDictionary();
            o.Add("team_1", id);
            o.Add("team_2", other.id);
            EventManager.Emit("collector:merge", o);

            capacity += (int)(other.capacity * 0.5);
            collected += (int)(other.collected * 0.5);
            collectRadius += (int)(other.collectRadius * 0.5f);
            targetScale += (other.scale * 0.3f);
            numCollectors += other.numCollectors;

            foreach (User u in other.users)
            {
                u.collector = this;
                users.Add(u);
            }

            health = 100;

            TeamColour.Put(other.teamColour);
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
                    if (!enemy.isDying)
                    {
                        projectiles.Add(new Projectile(enemy, this));
                        Audio.Play("collector.weapon", display);
                        collected--;
                        Console.WriteLine("Attacking...");
                    }
                }
            }
        }

        public void Collect()
        {
            if (!isDying)
            {
                if (!isPlayingAudio)
                {
                    Audio.Play("collector.resource" + ((collected % 4) + 1), display);
                    isPlayingAudio = true;
                    audioTimeout.Start();
                }
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
            Spritesheet[] collectorAnimations = {
                new Spritesheet("collector_intro", new Point(350, 600), (int)States.Intro, 48, false, (int)States.Static, true),
                new Spritesheet("collector_static", new Point(350, 600), (int)States.Static, 3, false, -1, false),
                new Spritesheet("collector_outro_01", new Point(350, 600), (int)States.Outro1, 18, false, -1, true), 
                new Spritesheet("collector_outro_02", new Point(350, 600), (int)States.Outro2, 18, false, -1, true), 
                new Spritesheet("collector_outro_03", new Point(350, 600), (int)States.Outro3, 18, false, -1, true), 
                new Spritesheet("collector_explosion", new Point(450, 600), (int)States.Exploding, 32, false, -1, true),
            };

            collectorAnim = new AnimSet(collectorAnimations);
            collectorAnim.frameOffset = new Vector2(0, 135);
            collectorAnim.Play((int)States.Intro);


            Animation[] portalAnimations = {
                new Animation(0, 47, false)
            };

            portalAnim = new AnimSprite("collector_portal", new Point(570, 200), portalAnimations);
            portalAnim.frameOffset.Y += 620;
            portalAnim.Play(0);


            Animation[] poofAnimations = {
                new Animation(0, 30, false)
            };

            poofAnim = new AnimSprite("collector_poof", new Point(200, 150), poofAnimations);
            poofAnim.playing = false;


            Animation[] explodeAnimations = {
                new Animation(0, 48, false)
            };

            explodeAnim = new AnimSprite("collector_explosion_particles", new Point(500, 500), explodeAnimations);
            explodeAnim.playing = false;
            explodeAnim.frameOffset.Y += 100;
        }
    }
}
