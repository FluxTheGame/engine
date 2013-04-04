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

        public delegate void Callback();
        public Vector2 frameOffset;

        private Callback animDone;
        private Callback sheetDone;
        private float rotation;
        private float delay;
        private bool playing = true;
        private int sequence = 0;
        private int rowOffset = 0;
        private int cols;


        public AnimSprite(string spriteName, Point frameSize, Animation[] animation)
        {
            delay = 1 / 24f;
            spriteBatch = ScreenManager.spriteBatch;
            frameSchedule = new Schedualizer(0.0f, delay, delay);
            sprite = ContentManager.Sprite(spriteName);
            frameOffset = new Vector2(frameSize.X, frameSize.Y) * 0.5f;
            cols = sprite.Width / frameSize.X;

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
                    if (animDone != null) animDone();
                }
                else if (animation[sequence].frame >= cols)
                {
                    int overshoot = animation[sequence].frame - cols + 1;
                    rowOffset = (int)Math.Ceiling((double)overshoot / (double)cols);
                }
            }
        }

        public void Draw(Color tint, float scale)
        {
            Rectangle clipping = new Rectangle((int)frameSize.X * (animation[sequence].frame - cols * rowOffset), (int)frameSize.Y * (sequence + rowOffset), frameSize.X, frameSize.Y);
            spriteBatch.Draw(sprite, position, clipping, tint, rotation, frameOffset, scale, SpriteEffects.None, 0f);
        }

        public void Draw(Color tint)
        {
            Draw(tint, 1f);
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
            else
            {
                playing = false;
                if (sheetDone != null) sheetDone();
            }
        }

        public void WhenFinished(Callback cb)
        {
            animDone = cb;
        }

        public void WhenSheetFinished(Callback cb)
        {
            sheetDone = cb;
        }

        public void Play(int clip, bool rewind = true)
        {
            playing = true;
            SetClip(clip);
            if (rewind) Rewind();
        }
    }
}
