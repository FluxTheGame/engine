package Screen {

	import Screen.*;

	public class ScreenController implements IController {
		
		private var _users:Array = [];
		private var _plants:Array = [];
		private var _climates:Array = [];
		private var _environments:Array = [];
		private var _animals:Array = [];		
		
		public function ScreenController() {
			for (var i:int=0; i<4; i++) {
				_users.push(new User(i));
				_environments.push(new Environment(i));
				_climates.push(new Climate(i));
			}
			
			_plants.push(new Plant());
			_animals.push(new Animal());
		}

		public function run():void {

			trace(); 

			for (var i:int=0; i<4; i++) {
				_users[i].update();
				_users[i].draw();
			}

			trace();

			for (var i:int=0; i<4; i++) {
				_environments[i].update();
				_environments[i].draw();
			}

			trace();

			for (var i:int=0; i<4; i++) {
				_climates[i].update();
				_climates[i].draw();
			}

			trace();

			for (var i:int=0; i<1; i++) {
				_plants[i].update();
				_plants[i].draw();
			}

			trace();

			for (var i:int=0; i<1; i++) {
				_animals[i].update();
				_animals[i].draw();
			}
		}
	}
}