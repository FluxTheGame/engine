package Screen {

	import flash.display.MovieClip;

	public class Animal extends MovieClip implements IScreenObject {

		public function update():void {
			trace("[SCREEN] Animal updated");
		}

		public function draw():void {
			trace("[SCREEN] Animal drawn");
		}

		public function getState():String {
			return "Animal";
		}

	}
}