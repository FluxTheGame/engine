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
    class ContentManager
    {

        public static Model collector;
        public static Model user;
        public static Model enemy;
        public static Model resource;
        public static Model wormhole;

        public static void Initialize(Game game)
        {
            collector = game.Content.Load<Model>(@"models/chicken");
            user = game.Content.Load<Model>(@"models/chicken");
            enemy = game.Content.Load<Model>(@"models/chicken");
            resource = game.Content.Load<Model>(@"models/chicken");
            wormhole = game.Content.Load<Model>(@"models/chicken");
        }
    }
}
