package Screen {

	import flash.display.MovieClip;

	public class Climate extends MovieClip implements IScreenObject {

		public var id:int;
		
		public function Climate(pid:int) {
			this.id = pid;
		}

		public function update():void {
			trace("[SCREEN]\tClimate #"+this.id+" updated");
		}

		public function draw():void {
			trace("[SCREEN]\tClimate #"+this.id+" drawn");
		}

		public function getState():String {
			return "Climate";
		}

	}
}