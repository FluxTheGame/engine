package Screen {
	public class Climate implements IScreenObject {

		public var id:int;
		
		public function Climate(pid:int) {
			this.id = pid;
		}

		public function update():void {
			trace("[SCREEN]\tClimate updated");
		}

		public function draw():void {

		}

		public function getState():String {
			return "Climate";
		}

	}
}