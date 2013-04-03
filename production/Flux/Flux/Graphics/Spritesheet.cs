using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Spritesheet
    {
        public int totalFrames;
        public int frame = 0;
        public Point frameSize;
        public AnimSprite animSprite;
        public int next;
        public int index;
        public bool play;

        protected Animation[] animations;



        public Spritesheet(string sheetName, int totalFrames, Point frameSize, int next = -1, int start = 0, bool play = true)
        {
            Texture2D spritesheet = ContentManager.Sprite(sheetName);
            this.totalFrames = totalFrames;
            this.frameSize = frameSize;
            this.play = play;

            int rows = spritesheet.Height / frameSize.Y;
            int cols = spritesheet.Width / frameSize.X;

            animations = new Animation[rows];

            for (int i = 0; i < rows; i++)
            {
                Animation animation;
                if (i + 1 < rows) animation = new Animation(i, cols, false, i + 1);
                else animation = new Animation(i, totalFrames % cols, false);

                animations[i] = animation;
            }

            animSprite = new AnimSprite(sheetName, frameSize, animations);
            animSprite.SetFrame(start);
        }

        public void Play(AnimSprite.Callback finished)
        {
            animSprite.WhenSheetFinished(finished);
            animSprite.Play(0, false);
        }

    }
}
