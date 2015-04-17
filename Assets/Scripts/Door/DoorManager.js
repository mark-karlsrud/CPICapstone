#pragma strict
var leftChild : GameObject;
var rightChild : GameObject;
private var transformLeft : Vector3;
private var transformRight : Vector3;
private var endPosLeft : Vector3;
private var endPosRight : Vector3;
private var speed : float;
var key : boolean;
var leftRend : Renderer;
var rightRend : Renderer;
var closedLeft : Texture;
var closedRight : Texture;
var openLeft : Texture;
var openRight : Texture;


function Awake () {
//Set private variables here
//Exceptions: key
	transformLeft = leftChild.transform.localPosition;
	transformRight = rightChild.transform.localPosition;
	endPosLeft = transformLeft + Vector3(0,0,1);
	endPosRight = transformRight + Vector3(0,0,-1);
	speed = .05;
	key = false;
	
}

function Start () {
	leftRend.material.mainTexture = closedLeft;
	rightRend.material.mainTexture = closedRight;
}

function Update () {
	if(key){
		leftChild.transform.localPosition = Vector3.Lerp(leftChild.transform.localPosition, endPosLeft, speed);
		leftRend.material.mainTexture = openLeft;
		leftRend.material.color = Color.green;
		rightChild.transform.localPosition = Vector3.Lerp(rightChild.transform.localPosition, endPosRight, speed);
		rightRend.material.mainTexture = openRight;
		rightRend.material.color = Color.green;
	}
	//Remove this else if the door doesn't close after button is pressed.
	/*else{
		leftChild.transform.localPosition = Vector3.Lerp(leftChild.transform.localPosition, transformLeft, speed);
		leftRend.material.mainTexture = closedLeft;
		leftRend.material.color = Color.white;
		rightChild.transform.localPosition = Vector3.Lerp(rightChild.transform.localPosition, transformRight, speed);
		rightRend.material.mainTexture = closedRight;
		rightRend.material.color = Color.white;
	}*/
}
