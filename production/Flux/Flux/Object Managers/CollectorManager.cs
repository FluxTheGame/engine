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
    public class CollectorManager : Manager
    {

        public static CollectorManager instance;
        List<Collector> collectors;

        
        public CollectorManager(Game game) : base(game)
        {
            CollectorManager.instance = this;
        }


        public override void Initialize()
        {
            collectors = new List<Collector>();

            EventManager.On("collector:join", (o) =>
            {
                int id = (int)o["id"];
                if (id > 0) collectors.Add(new Collector(id));
            });

            EventManager.On("collector:attack", (o) =>
            {
                Collector collector = CollectorByID((int)o["id"]);
                if (collector != null) collector.Attack();
            });

            base.Initialize();
        }


        public override void UpdateEach(int i)
        {
            CheckMerged(collectors[i]);
        }


        private void CheckMerged(Collector current)
        {
            for (int i = collectors.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(current.position, collectors[i].position) < 20 && current != collectors[i])
                {
                    current.MergeWith(collectors[i]);
                    collectors.Remove(collectors[i]);
                }
            }
        }

        public Collector CollectorByID(int id)
        {
            return collectors.FirstOrDefault(c => c.id == id);
        }

        public static void CheckEnemyCollide(Enemy enemy)
        {
            for (int i = instance.collectors.Count - 1; i >= 0; i--)
            {
                if (GameObject.Collides(enemy, instance.collectors[i]))
                {
                    enemy.Kamikaze(instance.collectors[i]);
                }
            }
        }

        //Used for Testing only - Remove
        public static Collector First()
        {
            return instance.collectors[0];
        }
        //End used for testing

        public static void Remove(Collector collector)
        {
            instance.collectors.Remove(collector);
        }

        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = collectors.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }

    }
}
