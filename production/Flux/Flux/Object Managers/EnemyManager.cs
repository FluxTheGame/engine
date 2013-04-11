using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{
    
    public class EnemyManager : Manager
    {

        public int desired = 20;
        public int reAddThreshold = 5;

        public static EnemyManager instance;

        protected List<Enemy> enemies;
        protected List<Enemy> addBuffer;

        
        public EnemyManager(Game game) : base(game)
        {
            EnemyManager.instance = this;
        }


        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Dictionary<string, string> audioFiles = new Dictionary<string, string>();
            audioFiles.Add("enemy.death", "sfx/enemy/enemy_death.wav");
            audioFiles.Add("enemy.spawn1", "sfx/enemy/enemy_spawn_1.wav");
            audioFiles.Add("enemy.spawn2", "sfx/enemy/enemy_spawn_2.wav");
            audioFiles.Add("enemy.spawn3", "sfx/enemy/enemy_spawn_3.wav");

            Audio.Load(audioFiles);

            enemies = new List<Enemy>();
            addBuffer = new List<Enemy>();
            
            base.LoadContent();
        }

        public static Enemy InRange(Collector collector)
        {
            foreach (Enemy enemy in instance.enemies)
            {
                if (GameObject.Distance(enemy, collector) < collector.attackRadius)
                {
                    return enemy;
                }
            }
            return null;
        }

        public static void Remove(Enemy enemy)
        {
            instance.enemies.Remove(enemy);
        }

        public void AddEnemiesType(string type, int numToAddToWorld)
        {
            for (int i = 0; i < numToAddToWorld; i++)
            {
                switch (type)
                {
                    case "bulger":
                        addBuffer.Add(new EnemyBulger());
                        break;

                    case "shooter":
                        addBuffer.Add(new EnemyShooter());
                        break;

                    case "crazy":
                        addBuffer.Add(new EnemyCrazy());
                        break;
                }
            }
        }

        public void AddEnemies()
        {
            int difference = desired - (enemies.Count + addBuffer.Count);

            if (difference > reAddThreshold)
            {
                AddEnemiesType("bulger", (int)(difference * 0.3));
                AddEnemiesType("shooter", (int)(difference * 0.3));
                AddEnemiesType("crazy", (int)(difference * 0.3));
            }
        }

        protected void MakeLive()
        {
            for (int i = addBuffer.Count - 1; i >= 0; i--)
            {
                if (addBuffer[i].addDelay.IsOn())
                {
                    enemies.Add(addBuffer[i]);
                    addBuffer[i].Activate();
                    addBuffer.RemoveAt(i);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            AddEnemies();
            MakeLive();

            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = enemies.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }

    }
}
