using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Spritesheet : Animation
    {

        public string sheet;
        public Point frameSize;
        public bool autoplay;

        public Spritesheet(string sheet, Point frameSize, int index, int totalFrames, bool loop = false, int next = -1, bool autoplay = false) 
            :  base(index, totalFrames, loop, next)
        {
            this.sheet = sheet;
            this.frameSize = frameSize;
            this.autoplay = autoplay;
        }
    }
}
