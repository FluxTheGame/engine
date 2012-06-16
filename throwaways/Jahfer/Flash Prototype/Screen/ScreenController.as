package Screen {

	import Screen.*;

	public class ScreenController implements IController {
		
		public var user:User = new User(0);
		
		public function ScreenController() {
			
		}

		public function run():void {
			user.update();
		}
	}
}