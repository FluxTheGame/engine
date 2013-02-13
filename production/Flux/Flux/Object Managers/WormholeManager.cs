﻿using System;
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
        protected List<WormholePair> wormholePairs;


        public WormholeManager(Game game) : base(game)
        {
            WormholeManager.instance = this;
        }

        public override void Initialize()
        {
            wormholePairs = new List<WormholePair>();
            base.Initialize();
        }

        public static void Add(Vector2 position, bool inward)
        {
            bool make = true;
            foreach (WormholePair w in instance.wormholePairs)
            {
                make = w.Make(position);
                if (!make) break;
            }

            if (make) {
                WormholePair wormholePair = new WormholePair(inward, position);
                instance.wormholePairs.Add(wormholePair);
            }
        }

        public static void Suck(GameObject passenger)
        {
            foreach (WormholePair w in instance.wormholePairs)
            {
                w.Suck(passenger);
            }
        }

        public static void Remove(WormholePair wormholePair)
        {
            instance.wormholePairs.Remove(wormholePair);
        }

        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = wormholePairs.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }
    }
}
