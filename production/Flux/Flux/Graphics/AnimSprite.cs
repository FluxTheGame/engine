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
        private Vector2 frameOffset;
        private Texture2D sprite;
        private Vector2 position;
        private Animation[] animation;

        public delegate void Callback();
        private Callback callback;
        private float rotation;
        private float delay;
        private bool playing = true;
        private int sequence = 0;


        public AnimSprite(string spriteName, Point frameSize, Animation[] animation)
        {
            delay = 1 / 24f;
            spriteBatch = ScreenManager.spriteBatch;
            frameSchedule = new Schedualizer(0.0f, delay, delay);
            sprite = ContentManager.Sprite(spriteName);
            frameOffset = new Vector2(frameSize.X, frameSize.Y) * 0.5f;

            this.animation = animation;
            this.frameSize = frameSize;
        }

        public void Update(Vector2 position, float rotation = 0f)
        {
            this.position = position;
            this.rotation = rotation;

            if (frameSchedule.IsOn() && playing)
            {
                animation[sequence].frame++;
                if (animation[sequence].frame >= animation[sequence].totalFrames)
                {
                    if (animation[sequence].loop) Rewind();
                    else Finish();
                    if (callback != null) callback();
                }
            }
        }

        public void Draw(Color tint)
        {
            Rectangle clipping = new Rectangle((int)frameSize.X * animation[sequence].frame, (int)frameSize.Y * sequence, frameSize.X, frameSize.Y);
            spriteBatch.Draw(sprite, position, clipping, tint, rotation, frameOffset, 1f, SpriteEffects.None, 0f);
        }

        public void Draw()
        {
            Draw(Color.White);
        }

        public void SetFrame(int frame)
        {
            animation[sequence].frame = frame;
            playing = false;
        }

        public void SetClip(int clip)
        {
            sequence = clip;
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

        public void WhenFinished(Callback cb)
        {
            callback = cb;
        }

        public void Play(int clip)
        {
            playing = true;
            SetClip(clip);
            Rewind();
        }
    }
}
