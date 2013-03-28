using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    /* =====================
     *  TWEENERIZER
     * =====================
     *  start 		=> initial value 
     *  end   		=> desired end value (may reach asymptote, be careful!)
     *  timestep 	=> the interpolated value of time, between 0 
     * 				   and duration (which is usually 1)
     *  [duration]	=> (optional) the unit of time to interpolate until
     *
     *
     *  EX. THE BORING WAY
     * ---------------------
     *  interp = 0f;
     *  do
     *  {
     *		float updatedValue = Tweenerizer.EaseInOut(0, 10, interp);
     *  } 
     *  while ((interp += 0.01f) < 1f);
     *
     * 
     *  OR THE AWESOME WAY
     * ---------------------
     *  Tweenerizer.Ease(EasingType.EaseInOut, minSize, maxSize, speedInMs, (ease) =>
     *  {
     *      updatedValue = ease;
     *  });
     * 
     */

    public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut, EaseOutIn };

    class Tweenerizer
    {
        // Callback for tweening
        public delegate void Callback(
            float ease /*incremented value between start and end*/,
            float incr /*amount value was last incremented by*/);

        private static List<TweenObject> tweens = new List<TweenObject>();

        // What you should be calling
        public static void Ease(EasingType type, float start, float end, float speed, Callback callback)
        {
            tweens.Add(new TweenObject(type, start, end, callback, speed));
        }

        public static void Update()
        {
            for (int i = 0; i < tweens.Count; ++i)
            {
                if (tweens[i].Update())
                {
                    tweens.Remove(tweens[i]);
                    i--;
                }
            }
        }


        /* === EASING FUNCTIONS AHOY! ========================================================= */

        // Constant speed
        public static float Linear(float start, float end, float timestep, float duration=1)
        {
            return end * timestep / duration + start;
        }

        // Accelerate
        public static float EaseIn(float start, float end, float timestep, float duration=1)
        {
            timestep /= duration;
            return end * timestep * timestep + start;
        }

        // Decelerate
        public static float EaseOut(float start, float end, float timestep, float duration=1)
        {
            timestep /= duration;
            return -end * timestep * (timestep - 2) + start;
        }

        // Accelerate then decelerate
        public static float EaseInOut(float start, float end, float timestep, float duration=1)
        {
            timestep /= duration / 2;
            if (timestep < 1) return end / 2 * timestep * timestep + start;
            timestep--;
            return -end / 2 * (timestep * (timestep - 2) - 1) + start;
        }

        // Decelerate then accelerate
        public static float EaseOutIn(float start, float end, float timestep, float duration=1)
        {
            float halfEnd = end / 2;

            if (timestep < duration / 2)
            {
                return EaseOut(start, halfEnd, timestep * 2);
            }

            return EaseIn(start + halfEnd, end, (timestep * 2) - duration);
        }
    }
}
