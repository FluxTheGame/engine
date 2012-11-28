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

            for (int i = 0; i < 3; i++)
            {
                Collector c = new Collector();
                c.position = new Vector2((i + 1) * 150, (i + 1) * 150);
                collectors.Add(c);
            }

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
                if (Vector2.Distance(current.position, collectors[i].position) < 10 && current != collectors[i])
                {
                    current.MergeWith(collectors[i]);
                    collectors.RemoveAt(i);
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
