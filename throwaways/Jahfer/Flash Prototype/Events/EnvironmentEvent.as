package Events {

	import flash.events.Event;

	public class EnvironmentEvent extends Event {

		public static const UPDATE_CLIMATE:String = "EnvironmentEvent.Update_Climate";
		public static const UPDATE_SCENE:String = "EnvironmentEvent.Update_Scene";

		public var args:*;

		public function EnvironmentEvent(type:String, args:*=null, bubbles:Boolean=true, cancelable:Boolean=false) {
			super(type, bubbles, cancelable);
			this.args = args;
		}

		public override function clone():Event {
			return new EnvironmentEvent(type, args, bubbles, cancelable);
		}

		public override function toString():String {
			return formatToString("EnvironmentEvent", "type", "args", "bubbles", "cancelable", "eventPhase");
		}
	}
}