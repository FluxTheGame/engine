package Screen {
	public class Environment implements IScreenObject {

		public var id:int;
		
		public function Environment(pid:int) {
			this.id = pid;
		}
		
		public function update():void {
			trace("[SCREEN]\tEnvironment updated");
		}

		public function draw():void {

		}

		public function getState():String {
			return "Environment";
		}

	}
}