package Logic {
	public class FoodChain implements ILogicObject {

		public function update():void {
			trace("> Food chain updated");
		}

		public function getState():String {
			return "FoodChain";
		}

	}
}