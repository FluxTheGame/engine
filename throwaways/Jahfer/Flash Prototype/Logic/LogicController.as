package Logic {

	import Logic.*;
	import Logic.Environment.EnvironmentController;

	public class LogicController implements IController {
		
		public var envController:IController = new EnvironmentController();
		
		public var comm:ILogicObject 		 = new Communication();
		public var foodChain:ILogicObject = new FoodChain();

		public function run():void {
			comm.update();
			foodChain.update();
			envController.run();
		}
	}
}