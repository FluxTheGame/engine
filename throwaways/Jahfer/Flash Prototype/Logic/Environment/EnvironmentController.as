package Logic.Environment {
	
	public class EnvironmentController implements IController {
		
		private var _environments:Array = [];
		private var _climates:Array 	 = []; 
		
		public function EnvironmentController() {
			for (var i:int=0; i<4; i++) {
				_environments.push(new Environment(i));
				_climates.push(new Climate(i));
			}
		}	
		
		public function getEnvironment(id:int):Environment {
			return _environments[id];
		}	
		
		public function getClimate(id:int):Climate {
			return _climates[id];
		}
		
		public function run():void {
			for(var i:int=0; i<4; i++) {
				_environments[i].update();
				_climates[i].update();
			}
		}                         
	}
}