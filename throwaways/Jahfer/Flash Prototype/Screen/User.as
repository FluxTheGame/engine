 package Screen {
	 
	import Screen.User;
	import Events.UserEvent;
	import flash.display.MovieClip;
	import EventManager;
	 
	public class User extends MovieClip implements IScreenObject {
		
		private var _selectedElement:String;
		public var currentEnvironment:int;
		public var id:int;
		
		public function User(pid:int) {
			this.x = this.y = 100;
			this._selectedElement = "Fire";
			this.currentEnvironment = 0;
			this.id = pid;
			
			EventManager.addEventListener(UserEvent.SELECT_ELEMENT, setSelectedElement);
			EventManager.addEventListener(UserEvent.UPDATE_POSITION, updatePosition);
		}
		
		public function releaseElement():void {
			// fire event listener
			trace("[SCREEN]\t" + this._selectedElement + " released");
		}
		
		public function setSelectedElement(e:UserEvent):void {
			//this._selectedElement = element;
			trace("\t[SCREEN]\tUser #"+this.id+" selected element is now " + this._selectedElement);
		}
		
		public function updatePosition(e:UserEvent):void {
			trace("\t[SCREEN]\tUser #"+this.id+" position updated to ("+this.x+", "+this.y+")");
		}

		public function update():void {
			trace("[SCREEN]\tUser #"+this.id+" @ ("+this.x+", "+this.y+")");
		}

		public function draw():void {
			
		}

		public function getState():String {
			return "User";
		}

	}
}