package {
	import flash.events.EventDispatcher;
 	import flash.events.Event;

  	public class EventManager {
		protected static var evdDispatcher:EventDispatcher;

	 	public static function addEventListener(Type:String, Listener:Function, Capture:Boolean=false, Priority:int=0, WeakReference:Boolean=false):void {
		 	
			if (evdDispatcher == null) { 
				evdDispatcher = new EventDispatcher(); 
			}

			evdDispatcher.addEventListener(Type, Listener, Capture, Priority, WeakReference);
		}

	 	public static function removeEventListener(Type:String, Listener:Function, Capture:Boolean=false):void {
			if (evdDispatcher == null) { return; }

			evdDispatcher.removeEventListener(Type, Listener, Capture);
		}

	 	public static function dispatchEvent(e:Event):void {
			if (evdDispatcher == null) { return; }

			evdDispatcher.dispatchEvent(e);
		}
	}
}