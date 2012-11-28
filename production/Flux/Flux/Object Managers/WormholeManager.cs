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
    public class WormholeManager : Manager
    {

        public static WormholeManager instance;
        protected List<Wormhole> wormholes;


        public WormholeManager(Game game) : base(game)
        {
            WormholeManager.instance = this;
        }


        public override void Initialize()
        {
            wormholes = new List<Wormhole>();
            base.Initialize();
        }


        public static void Add(Vector2 position, bool inward)
        {
            bool make = true;
            foreach (Wormhole w in instance.wormholes)
            {
                if (Vector2.Distance(w.position, position) < 150) make = false;
            }

            if (make) {
                Wormhole wormhole = new Wormhole(inward);
                wormhole.position = position;
                instance.wormholes.Add(wormhole);
            }
        }

        public static void Remove(Wormhole wormhole)
        {
            instance.wormholes.Remove(wormhole);
        }

        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = wormholes.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }

    }
}
