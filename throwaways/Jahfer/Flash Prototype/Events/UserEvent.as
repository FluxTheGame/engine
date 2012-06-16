package Events {

	import flash.events.Event;

	public class UserEvent extends Event {

		public static const SELECT_ELEMENT:String = "UserEvent.Select_Element";
		public static const UPDATE_POSITION:String = "UserEvent.Update_Position";

		public var args:*;

		public function UserEvent(type:String, args:*=null, bubbles:Boolean=true, cancelable:Boolean=false) {
			super(type, bubbles, cancelable);
			this.args = args;
		}

		public override function clone():Event {
			return new UserEvent(type, args, bubbles, cancelable);
		}

		public override function toString():String {
			return formatToString("UserEvent", "type", "args", "bubbles", "cancelable", "eventPhase");
		}
	}
}