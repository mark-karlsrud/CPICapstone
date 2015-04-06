var player : GameObject;

function OnTriggerEnter(object : Collider){
	if(object == player.collider){
		var trap = GameObject.Find("Trap1");
		trap.GetComponent(StructureManager).flag = true;
		Destroy(this);

	}
}