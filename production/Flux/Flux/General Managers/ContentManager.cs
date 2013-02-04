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
        public static Model resource;
        public static Model wormhole;

        public static Model enemyBulger;
        public static Model enemyShooter;
        public static Model enemyCrazy;

        public static Texture2D user;
        public static Texture2D userPoints;
        public static Texture2D userBox;
        public static SpriteFont userfont;


        public static void Initialize(Game game)
        {
            collector = game.Content.Load<Model>(@"models/collector");
            resource = game.Content.Load<Model>(@"models/chicken");
            wormhole = game.Content.Load<Model>(@"models/chicken");

            enemyBulger = game.Content.Load<Model>(@"models/enemy_bulger");
            enemyShooter = game.Content.Load<Model>(@"models/enemy_shooter");
            enemyCrazy = game.Content.Load<Model>(@"models/enemy_crazy");

            user = game.Content.Load<Texture2D>(@"sprites/cursor_arrow");
            userPoints = game.Content.Load<Texture2D>(@"sprites/cursor_circle");
            userBox = game.Content.Load<Texture2D>(@"sprites/cursor_box");
            userfont = game.Content.Load<SpriteFont>(@"fonts/font");
        }
    }
}
