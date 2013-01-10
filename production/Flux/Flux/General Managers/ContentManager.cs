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

        public static Model collectorSm;
        public static Model collectorM;
        public static Model collectorLg;
        public static Model user;
        public static Model enemy;
        public static Model resource;
        public static Model wormhole;

        public static void Initialize(Game game)
        {
            //collectorSm = game.Content.Load<Model>("collector_sm");
            //collectorM = game.Content.Load<Model>("collector_m");
            //collectorLg = game.Content.Load<Model>("collector_lg");
            user = game.Content.Load<Model>(@"models/chicken");
            //enemy = game.Content.Load<Model>("enemy");
            //resource = game.Content.Load<Model>("resource");
            //wormhole = game.Content.Load<Model>("wormhole");
        }
    }
}
