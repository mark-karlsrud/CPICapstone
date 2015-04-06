var player : GameObject;
var door : GameObject;

function OnTriggerEnter(object : Collider){
	if(object == player.collider){
		var buttonTop = GameObject.Find("Button");
		buttonTop.GetComponent(ButtonManager).isDown = true;
		//If there is a door
		if(door != null){
			var openthatshitup = door;
			openthatshitup.GetComponent(DoorManager).key = true;
		}
	}
}

function OnTriggerExit(object : Collider){
	if(object == player.collider){
		var buttonTop = GameObject.Find("Button");
		buttonTop.GetComponent(ButtonManager).isDown = false;
		//If there is a door
		if(door != null){
			var openthatshitup = door;
			openthatshitup.GetComponent(DoorManager).key = false;
		}	
	}
}