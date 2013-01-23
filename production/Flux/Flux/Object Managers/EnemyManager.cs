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
            enemies = new List<Enemy>();

            for (int i = 0; i < 3; i++)
            {
                enemies.Add(new EnemyBulger());
            }

            base.Initialize();
        }

        public static void Attack(Collector collector)
        {
            Enemy closest = null;
            foreach (Enemy enemy in instance.enemies)
            {
                if (closest == null) closest = enemy;
                else
                {
                    float distance1 = Vector2.Distance(collector.position, enemy.position);
                    float distance2 = Vector2.Distance(collector.position, closest.position);
                    if (distance1 < distance2) closest = enemy;
                }
            }
            closest.BeAttacked();
        }


        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = enemies.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }

    }
}
