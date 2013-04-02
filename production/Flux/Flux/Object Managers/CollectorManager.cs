using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

            EventManager.On("collector:new", (o) =>
            {
                int id = (int)o["id"];
                collectors.Add(new Collector(id));
            });

            EventManager.On("collector:attack", (o) =>
            {
                Collector collector = CollectorByID((int)o["id"]);
                if (collector != null) collector.Attack();
            });

            EventManager.On("collector:destroy", (o) =>
            {
                Collector collector = CollectorByID((int)o["id"]);
                if (collector != null) collector.Die();
            });

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Dictionary<string, string> audioFiles = new Dictionary<string, string>();
            audioFiles.Add("collector.complete", "sfx/collector/collector_complete.wav");
            audioFiles.Add("collector.death", "sfx/collector/collector_death.wav");
            audioFiles.Add("collector.merge", "sfx/collector/collector_merge.wav");
            audioFiles.Add("collector.resource1", "sfx/collector/collector_resource_inserted_1.wav");
            audioFiles.Add("collector.resource2", "sfx/collector/collector_resource_inserted_2.wav");
            audioFiles.Add("collector.resource3", "sfx/collector/collector_resource_inserted_3.wav");
            audioFiles.Add("collector.resource4", "sfx/collector/collector_resource_inserted_4.wav");
            audioFiles.Add("collector.spawn", "sfx/collector/collector_spawn.wav");
            audioFiles.Add("collector.weapon", "sfx/collector/collector_weapon.wav");

            Audio.Load(audioFiles);
            
            base.LoadContent();
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
                    if (current.scale > collectors[i].scale) current.MergeWith(collectors[i]);
                    else collectors[i].MergeWith(current);
                    break;
                }
            }
        }

        public static Collector CollectorByID(int id)
        {
            return instance.collectors.FirstOrDefault(c => c.id == id);
        }

        public static void CheckEnemyCollide(Enemy enemy)
        {
            for (int i = instance.collectors.Count - 1; i >= 0; i--)
            {
                if (GameObject.Distance(enemy, instance.collectors[i]) < 50)
                {
                    enemy.Kamikaze(instance.collectors[i]);
                }
            }
        }

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
