using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Animation
    {
        public int index;
        public int next = -1;
        public int frame = 0;
        public int totalFrames;
        public bool loop = false;

        public Animation(int index, int totalFrames, bool loop = true, int next = -1)
        {
            this.index = index;
            this.totalFrames = totalFrames;
            this.loop = loop;

            if (next >= 0)
            {
                this.loop = false;
                this.next = next;
            }
        }
    }
}
