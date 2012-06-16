package Screen {
	public class Plant implements IScreenObject {

		public function update():void {
			trace("[SCREEN]\tPlant updated");
		}

		public function draw():void {

		}

		public function getState():String {
			return "Plant";
		}

	}
}