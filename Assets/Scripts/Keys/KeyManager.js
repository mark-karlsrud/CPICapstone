#pragma strict
var key : GameObject;
var keyAmount : int;
var keyList : GameObject[];
var startPos : Vector3;
var startingPositions : Vector3[]; //Set these in the inspector.
private var keyIn : boolean = false;
private var variableManager : GameObject;
private var door : GameObject;


function Start () {

	if(keyAmount == 0){
		//Do nothing
		throw new System.ArgumentException("keyAmount value set to 0. Please advise.");
	}
	else if(keyAmount == 1){//Create One Key
		CreateOneKey();
	}
	else if(keyAmount > 1){//Create Multiple Keys
		CreateManyKeys();	
	}
	else {
		//Do nothing
		throw new System.ArgumentException("Please check keyAmount value is non-negative.");
	}
//Get access to other components.	
	variableManager = GameObject.Find("GameManager");
	door = GameObject.FindGameObjectWithTag("Door");
	
}

function CreateManyKeys(){
	keyList = new GameObject[keyAmount];
	for(var i = 0; i < keyAmount; i++){
		var newKey : GameObject = GameObject.Instantiate(key ,startingPositions[i], Quaternion.identity); 
		keyList[i] = newKey;
	}
}

function CreateOneKey(){
	Instantiate(key, startPos, Quaternion.identity);
}

