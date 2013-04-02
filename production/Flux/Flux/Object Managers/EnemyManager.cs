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

        public static EnemyManager instance;
        List<Enemy> enemies;

        
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

            for (int i = 0; i < 1; i++)
            {
                enemies.Add(new EnemyBulger());
                enemies.Add(new EnemyShooter());
                enemies.Add(new EnemyCrazy());
            }

            
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


        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = enemies.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }

    }
}
