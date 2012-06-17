package Logic.Environment {
	
	import Logic.ILogicObject;
	import Events.EnvironmentEvent;

	public class Climate implements ILogicObject {
		
		private var _humidity:int;
		private var _temperature:int;
		public var id:int;
		
		public function Climate(pid:int) {
			this._humidity = 10;
			this._temperature = 30;
			this.id = pid;
			EventManager.addEventListener(EnvironmentEvent.UPDATE_CLIMATE, load);
		}
		
		public function load(e:EnvironmentEvent):void {
			trace("\t[LOGIC]\tClimate #"+this.id+" updated");
		}

		public function update():void {
			
		}

		public function getState():String {
			return "Climate";
		}

	}
}