package Logic.Environment {
	
	import Logic.ILogicObject;
	import Events.EnvironmentEvent;
	import EventManager;
	
	public class Environment implements ILogicObject {

		public var id:int;

		public function Environment(pid:int) {
			this.id = pid;
		}

		public function update():void {
			
			trace("> Environment #"+this.id+" updated");
		}

		public function getState():String {
			return "Environment";
		}

	}
}