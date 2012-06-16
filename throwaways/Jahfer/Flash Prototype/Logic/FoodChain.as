package Logic {
	public class FoodChain implements ILogicObject {

		public function update():void {
			trace("[LOGIC]\tFood chain updated");
		}

		public function getState():String {
			return "FoodChain";
		}

	}
}