private var startPos : Vector3;
var spawnStructure : GameObject;
var flag : boolean = false;
private var distance : float = 6;
 
function Start () {
	startPos = new Vector3(0.35, 4.11,-21);
	
}

function Update() {
	if(flag){
		for(var i = 0; i < 5; i++){
		var newStruct = GameObject.Instantiate(spawnStructure, startPos + (Vector3.forward * (distance * i)), Quaternion.identity);
		}
	}
	flag = false;
}
