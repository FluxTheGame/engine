using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    public class Schedualizer
    {
        protected DateTime next;
        public Double duration;
        public int minInterval;
        public int maxInterval;

        public Schedualizer(float duration, int minInterval, int maxInterval) : base()
        {
            this.duration = (Double)duration;
            this.minInterval = minInterval;
            this.maxInterval = maxInterval;
            next = DateTime.Now;
            AddTime();
        }

        public bool IsOn()
        {
            if (DateTime.Now.CompareTo(next) > 0) {
                if (DateTime.Now.CompareTo(next.AddSeconds(duration)) >= 0) {
                    AddTime();
                }
                return true;
            }
            return false;
        }

        public float Phase()
        {
            TimeSpan sinceStart = DateTime.Now.Subtract(next);
            float seconds = sinceStart.Ticks * 0.0000001f;
            if (duration > 0 && seconds > 0) return seconds/(float)duration;
            else return 0.0f;
        }

        protected void AddTime()
        {
            next = next.AddSeconds(Randomizer.RandomInt(minInterval, maxInterval));
        }

    }
}
