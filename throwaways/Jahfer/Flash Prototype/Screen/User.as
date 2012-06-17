 package Screen {
	 
	import Screen.User;
	import Events.UserEvent;
	import flash.display.MovieClip;
	import EventManager;
	 
	public class User extends MovieClip implements IScreenObject {
		
		private var _selectedElement:String;
		private var _screenLocation:int;
		public var id:int;
		
		public function User(pid:int) {
			this.x = this.y = 100;
			this._selectedElement = "Fire";
			this._screenLocation = 0;
			this.id = pid;
			
			EventManager.addEventListener(UserEvent.SELECT_ELEMENT, setSelectedElement);
			EventManager.addEventListener(UserEvent.UPDATE_POSITION, updatePosition);
		}
		
		public function releaseElement():void {
			// fire event listener
			trace("[SCREEN]\t" + this._selectedElement + " released");
		}
		
		public function setSelectedElement(e:UserEvent):void {
			if (e.args.userid != this.id) return;
			var newElement:String = e.args.selectedElement;
			this._selectedElement = newElement;
			//this._selectedElement = element;
			trace("\t[SCREEN]\tUser #"+this.id+" selected element is now " + this._selectedElement);
		}
		
		public function updatePosition(e:UserEvent):void {
			if (e.args.userid != this.id) return;
			this.x = e.args.x;
			this.y = e.args.y;
			this._screenLocation = e.args.screenLocation;
			trace("\t[SCREEN]\tUser #"+this.id+" position updated to ("+this.x+", "+this.y+", Screen "+this._screenLocation+")");
		}

		public function update():void {
			trace("[SCREEN]\tUser #"+this.id+" updated (Elem is "+this._selectedElement+")");
		}

		public function draw():void {
			trace("[SCREEN]\tUser #"+this.id+" drawn @ ("+this.x+", "+this.y+", Screen "+this._screenLocation+")");
		}

		public function getState():String {
			return "User";
		}

	}
}