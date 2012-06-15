package Screen {

	import Screen.*;

	public class ScreenController implements IController {
		var env:IScreenObject 	 = new Environment();
		var user:IScreenObject 	 = new User();
		var plant:IScreenObject  = new Plant();
		var animal:IScreenObject = new Animal();
		var climate:IScreenObject = new Climate();

		public function getInternalStates():void {
			trace("Screen: " + env.getState(), user.getState(), plant.getState(), animal.getState(), climate.getState() );
		}
	}
}