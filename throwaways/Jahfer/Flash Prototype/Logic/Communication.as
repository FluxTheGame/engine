package Logic {
	
	import Events.*;
	
	public class Communication implements ILogicObject {

		public function Communication():void {
			super();
		}
		
		public function update():void {
			trace("[LOGIC]\tRetrieved data from server");

			var elementInfo:Object = {
				userid: 0,
				selectedElement: "Wind"
			};

			var coords:Object = {
				userid: 0,
				x: 400,
				y: 50,
				screenLocation: 3
			};

			EventManager.dispatchEvent(new UserEvent(UserEvent.SELECT_ELEMENT, elementInfo));
			EventManager.dispatchEvent(new UserEvent(UserEvent.UPDATE_POSITION, coords));
			EventManager.dispatchEvent(new EnvironmentEvent(EnvironmentEvent.UPDATE_CLIMATE));
			EventManager.dispatchEvent(new EnvironmentEvent(EnvironmentEvent.UPDATE_SCENE));
			EventManager.dispatchEvent(new UserEvent(UserEvent.PLANT_SEEDED));
		}

		public function getState():String {
			return "Communication";
		}

	}
}