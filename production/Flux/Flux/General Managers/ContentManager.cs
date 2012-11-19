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

        public static Texture2D collectorSm;
        public static Texture2D collectorM;
        public static Texture2D collectorLg;
        public static Texture2D user;
        public static Texture2D enemy;
        public static Texture2D resource;
        public static Texture2D wormhole;

        public static void Initialize(Game game)
        {
            collectorSm = game.Content.Load<Texture2D>("collector_sm");
            collectorM = game.Content.Load<Texture2D>("collector_m");
            collectorLg = game.Content.Load<Texture2D>("collector_lg");
            user = game.Content.Load<Texture2D>("mouse");
            enemy = game.Content.Load<Texture2D>("enemy");
            resource = game.Content.Load<Texture2D>("resource");
            wormhole = game.Content.Load<Texture2D>("wormhole");
        }
    }
}
