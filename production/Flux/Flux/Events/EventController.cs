using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{

    // Delegates
    public delegate void NewGridObjectEvent();


    class EventController
    {
        public static NewGridObjectEvent NewGridObjectEvent;


    }
}
