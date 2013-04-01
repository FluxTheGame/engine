using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Flux
{
    public class EventManager
    {
        public delegate void Callback(OrderedDictionary o);
        private static Dictionary<string, Callback> _e;

        public static void Initialize()
        {
            _e = new Dictionary<string, Callback>();
        }


        /**
         * ====== USAGE ==================
         * EventManager.On("foo", (o) => {
         *      do cool stuff...
         * });
         * 
         * - OR -
         * 
         * EventManager.On("bar", MyFunc);
         */
        public static void On(string evt, Callback callback)
        {
            Callback cb = callback;

            if (_e.ContainsKey(evt))
            {
                _e[evt] += callback;
            }
            else
            {
                _e.Add(evt, cb);
            }
        }

        public static void Emit(string evt, OrderedDictionary data)
        {
            if (_e.ContainsKey(evt))
            {
                _e[evt](data);
            }
        }
    }
}
