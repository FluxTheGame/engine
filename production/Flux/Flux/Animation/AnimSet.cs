﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class AnimSet
    {

        protected Spritesheet[] spritesheets;
        protected int sheet = 0;
        protected AnimSprite anim;


        public AnimSet(Spritesheet[] spritesheets)
        {
            this.spritesheets = spritesheets;
        }

        public void Update(Vector2 position, float rotation = 0f)
        {
            anim.Update(position, rotation);

            if (anim.currentFrame >= spritesheets[sheet].totalFrames)
            {
                if (spritesheets[sheet].next >= 0)
                {
                    Play(spritesheets[sheet].next);
                }
            }
        }

        public void Draw()
        {
            Draw(Color.White, 1f);
        }

        public void Draw(Color tint, float scale)
        {
            anim.Draw(tint, scale);
        }

        public void Play(int sheet)
        {
            this.sheet = sheet;

            Animation[] animation = new Animation[] {
                new Animation(0, spritesheets[sheet].totalFrames, spritesheets[sheet].loop)
            };

            anim = new AnimSprite(spritesheets[sheet].sheet, spritesheets[sheet].frameSize, animation);
            Console.WriteLine(spritesheets[sheet].sheet);

            anim.SetFrame(0);
            if (spritesheets[sheet].autoplay) anim.Play(0);
        }
    }
}
