package Screen {

	import flash.display.MovieClip;

	public class Environment extends MovieClip implements IScreenObject {

		public var id:int;
		
		public function Environment(pid:int) {
			this.id = pid;
		}
		
		public function update():void {
			trace("[SCREEN]\tEnvironment #"+this.id+" updated");
		}

		public function draw():void {
			trace("[SCREEN]\tEnvironment #"+this.id+" drawn");
		}

		public function getState():String {
			return "Environment";
		}

	}
}