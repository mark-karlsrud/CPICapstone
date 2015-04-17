#pragma strict
var isDown : boolean;
private var startPos : Vector3; 
private var endPos : Vector3;
private var speed : float;
var button : GameObject;


function Start () {
	startPos = button.transform.localPosition;
	endPos = startPos - Vector3(0,1.8,0);
	isDown = false;
	speed = .05;
}

function Update () {
	if(isDown){
		button.transform.localPosition = Vector3.Lerp(endPos, startPos, speed);		
	}
	else{
		button.transform.localPosition = Vector3.Lerp(startPos, endPos, speed);
	}
}