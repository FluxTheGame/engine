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
        public int next;
        public int index;
        public bool play;



        public Spritesheet(string sheetName, int totalFrames, Point frameSize, int next = -1, bool play = true)
        {
            Texture2D spritesheet = ContentManager.Sprite(sheetName);
            this.totalFrames = totalFrames;
            this.frameSize = frameSize;
            this.play = play;

            int rows = spritesheet.Height / frameSize.Y;
            int cols = spritesheet.Width / frameSize.X;

            
        }

    }
}
