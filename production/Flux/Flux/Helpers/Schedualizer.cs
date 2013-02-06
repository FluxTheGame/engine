using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    public class Schedualizer
    {
        protected DateTime next;
        protected Double duration;
        protected float minInterval;
        protected float maxInterval;

        /* Usage
         * Schedualizer schedual = new Schedualizer(3.0f, 10.0f, 20.0f);
         * if (schedual.IsOn()) {
         *     //Do something while firing
         * }
         */

        public Schedualizer(float duration, float minInterval, float maxInterval) : base()
        {
            this.duration = (Double)duration;
            this.minInterval = minInterval;
            this.maxInterval = maxInterval;
            Start();
            AddTime();
        }

        public void Start()
        {
            next = DateTime.Now;
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
            next = next.AddSeconds(Randomizer.RandomFloat(minInterval, maxInterval));
        }

    }
}
