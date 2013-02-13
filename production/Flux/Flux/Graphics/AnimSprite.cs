using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{

    public class AnimSprite
    {

        private SpriteBatch spriteBatch;
        private Schedualizer frameSchedule;
        private Point frameSize;
        private Texture2D sprite;
        private Vector2 position;
        private Animation[] animation;

        private float delay;
        private bool playing = true;
        private int sequence = 0;


        public AnimSprite(string spriteName, Point frameSize, Animation[] animation)
        {
            delay = 1 / 24f;
            spriteBatch = ScreenManager.spriteBatch;
            frameSchedule = new Schedualizer(0.0f, delay, delay);
            sprite = ContentManager.Sprite(spriteName);

            this.animation = animation;
            this.frameSize = frameSize;
        }

        public void Update(Vector2 position)
        {
            this.position = position;

            if (frameSchedule.IsOn() && playing)
            {
                animation[sequence].frame++;
                if (animation[sequence].frame >= animation[sequence].totalFrames)
                {
                    if (animation[sequence].loop) Rewind();
                    else Finish();
                }
            }
        }

        public void Draw()
        {
            spriteBatch.Draw(sprite, position, new Rectangle((int)frameSize.X * animation[sequence].frame, (int)frameSize.Y * sequence, frameSize.X, frameSize.Y), Color.White);
        }

        public void SetFrame(int frame)
        {
            animation[sequence].frame = frame;
            playing = false;
        }

        public void Rewind()
        {
            animation[sequence].frame = 0;
        }

        public void Finish()
        {
            if (animation[sequence].next >= 0)
            {
                Play(animation[sequence].next);
            }
            else playing = false;
        }

        public void Play(int clip)
        {
            sequence = clip;
        }
    }
}
