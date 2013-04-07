using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    class TweenObject
    {
        private Tweenerizer.Callback callback;
        private EasingType tweenType;
        private float start;
        private float end;
        private float speed;
        private float lastVal = 0;
        // generic description of tweening function
        private Func<float, float, float, float, float> tweenFunc;
        private Action handleOnComplete;

        private float timestep = 0;

        public TweenObject(EasingType type, float start, float end, Tweenerizer.Callback callback, float speed = 1000, Action onComplete = null)
        {
            tweenType = type;
            this.start = start;
            this.end = end;
            this.callback = callback;
            this.speed = speed / 1000;
            handleOnComplete = onComplete;

            switch (tweenType)
            {
                default:
                    this.tweenFunc = Tweenerizer.Linear;
                    break;
                case EasingType.EaseIn:
                    this.tweenFunc = Tweenerizer.EaseIn;
                    break;
                case EasingType.EaseOut:
                    this.tweenFunc = Tweenerizer.EaseOut;
                    break;
                case EasingType.EaseInOut:
                    this.tweenFunc = Tweenerizer.EaseInOut;
                    break;
                case EasingType.EaseOutIn:
                    this.tweenFunc = Tweenerizer.EaseOutIn;
                    break;
            }
        }

        public bool Update()
        {
            timestep += 0.01f;

            float ease = tweenFunc(start, end, timestep, speed);
            callback(ease, ease-lastVal);
            lastVal = ease;

            if ((timestep - speed) >= 0)
            {
                if (handleOnComplete != null) handleOnComplete();
                return true;
            }

            return false;
        }
    }
}
