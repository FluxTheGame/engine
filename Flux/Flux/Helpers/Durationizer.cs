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

        public float phase;


        public Durationizer(float duration)
        {
            this.duration = (Double)duration;
            end = DateTime.Now;
            phase = 0f;
        }

        public void Fire()
        {
            if (!IsOn())
            {
                end = DateTime.Now;
                end = end.AddSeconds(duration);
            }
        }

        public bool IsOn()
        {
            Phase();
            if (DateTime.Now.CompareTo(end) < 0) return true;
            return false;
        }

        public float Phase()
        {
            TimeSpan toEnd = end.Subtract(DateTime.Now);
            this.phase = (float)toEnd.TotalSeconds / (float)duration;
            return this.phase;
        }
    }
}
