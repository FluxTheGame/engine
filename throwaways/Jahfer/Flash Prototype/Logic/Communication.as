package Logic {
	
	import Screen.User;
	import Events.*;
	import EventManager;
	
	public class Communication implements ILogicObject {

		public function Communication():void {
			super();
		}
		
		public function update():void {
			trace("[LOGIC]\tRetrieved data from server");
			EventManager.dispatchEvent(new UserEvent(UserEvent.SELECT_ELEMENT));
			EventManager.dispatchEvent(new UserEvent(UserEvent.UPDATE_POSITION));
			EventManager.dispatchEvent(new EnvironmentEvent(EnvironmentEvent.UPDATE_CLIMATE));
			EventManager.dispatchEvent(new EnvironmentEvent(EnvironmentEvent.UPDATE_SCENE));
		}

		public function getState():String {
			return "Communication";
		}

	}
}