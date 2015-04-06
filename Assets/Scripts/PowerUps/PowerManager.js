#pragma strict
var powerUp : GameObject;
var powerAmount : int;
var powerList : GameObject[];
var startPos : Vector3;
var startingPositions : Vector3[]; //Set these in the inspector.
private var keyIn : boolean = false;
private var variableManager : GameObject;
private var door : GameObject;


function Start () {

	if(powerAmount == 0){
		//Do nothing
		throw new System.ArgumentException("keyAmount value set to 0. Please advise.");
	}
	else if(powerAmount == 1){//Create One Key
		CreateOneKey();
	}
	else if(powerAmount > 1){//Create Multiple Keys
		CreateManyKeys();	
	}
	else {
		//Do nothing
		throw new System.ArgumentException("Please check keyAmount value is non-negative.");
	}
//Get access to other components.	
	variableManager = GameObject.Find("GameManager");	
}

function CreateManyKeys(){
	powerList = new GameObject[powerAmount];
	for(var i = 0; i < powerAmount; i++){
		var newPower : GameObject = GameObject.Instantiate(powerUp ,startingPositions[i], Quaternion.identity); 
		powerList[i] = newPower;
	}
}

function CreateOneKey(){
	Instantiate(powerUp, startPos, Quaternion.identity);
}

