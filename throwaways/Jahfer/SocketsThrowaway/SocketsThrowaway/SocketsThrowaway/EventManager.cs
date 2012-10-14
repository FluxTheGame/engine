using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Collections.Concurrent;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Microsoft.CSharp;

namespace SocketsThrowaway
{
    public class EventManager
    {
        public delegate void Callback(Object o);
        private static Dictionary<string, Callback> _e;

        public static void Initialize() {
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

        public static void Emit(string evt, object data)
        {
            if (_e.ContainsKey(evt))
            {
                _e[evt](data);
            }
        }
    }
}
