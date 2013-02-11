using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    public class Animation
    {
        public int index;
        public int next;
        public int frame = 0;
        public int totalFrames;
        public bool loop = true;

        public Animation(int index, int totalFrames, int next = -1)
        {
            this.index = index;
            this.totalFrames = totalFrames;
            if (next >= 0)
            {
                loop = false;
                this.next = next;
            }
        }
    }
}
