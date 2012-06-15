package Logic {

	import Logic.*;

	public class LogicController implements IController {
		var comm:ILogicObject 		= new Communication();
		var climate:ILogicObject 	= new Climate();
		var env:ILogicObject 		= new Environment();
		var foodchain:ILogicObject 	= new FoodChain();

		public function getInternalStates():void {
			trace("Logic: " + comm.getState(), climate.getState(), env.getState(), foodchain.getState() );
		}
	}
}