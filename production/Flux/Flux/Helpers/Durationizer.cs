using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    public class Durationizer
    {

        protected Double duration;
        protected DateTime end;


        public Durationizer(float duration)
        {
            this.duration = (Double)duration;
            end = DateTime.Now;
        }

        public void Fire()
        {
            if (!IsOn()) //Turn off this IF in production.. Only used for keyboard input
            {
                end = end.AddSeconds(duration);
            }
        }

        public bool IsOn()
        {
            if (DateTime.Now.CompareTo(end) < 0) return true;
            return false;
        }
    }
}
