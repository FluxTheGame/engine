package Screen {
	
	import flash.display.MovieClip;

	public class Plant extends MovieClip implements IScreenObject {

		public function update():void {
			trace("[SCREEN]\tPlant updated");
		}

		public function draw():void {
			trace("[SCREEN]\tPlant drawn");
		}

		public function getState():String {
			return "Plant";
		}

	}
}