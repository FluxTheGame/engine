package Screen {
	public class Animal implements IScreenObject {

		public function update():void {
			trace("[SCREEN] Animal updated");
		}

		public function draw():void {

		}

		public function getState():String {
			return "Animal";
		}

	}
}