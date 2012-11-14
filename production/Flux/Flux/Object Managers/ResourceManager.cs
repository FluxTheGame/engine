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

    public class ResourceManager : Manager
    {

        public static ResourceManager instance;
        protected List<Resource> resources;


        public ResourceManager(Game game) : base(game)
        {
            ResourceManager.instance = this;
        }


        public override void Initialize()
        {
            resources = new List<Resource>();

            for (int i = 0; i < 5000; i++) 
                resources.Add(new Resource());

            base.Initialize();
        }


        public static void Gather(Collector collector)
        {
            for (int i = instance.resources.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(instance.resources[i].position, collector.position) < collector.SuckRadius())
                {
                    collector.Collect(instance.resources[i]);
                    instance.resources.RemoveAt(i);
                }
            }
        }

        
        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = resources.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }

    }
}
