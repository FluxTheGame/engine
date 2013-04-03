using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class AnimSet
    {

        private Spritesheet[] spritesheets;
        private int sheet;


        public AnimSet(Spritesheet[] spritesheets)
        {
            this.spritesheets = spritesheets;
        }

        public void Update(Vector2 position, float rotation = 0f)
        {
            spritesheets[sheet].animSprite.Update(position, rotation);
        }

        public void Draw()
        {
            Draw(Color.White);
        }

        public void Draw(Color tint)
        {
            spritesheets[sheet].animSprite.Draw(tint);
        }

        public void Play(int sheet)
        {
            this.sheet = sheet;
            spritesheets[sheet].Play(() =>
            {
                if (spritesheets[sheet].next >= 0)
                {
                    sheet = spritesheets[sheet].next;
                }
            });
        }
    }
}
