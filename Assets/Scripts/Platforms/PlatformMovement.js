#pragma strict
var targetA : GameObject;
var targetB : GameObject;
private var startPos : Vector3;
private var targetAPos : Vector3;
private var targetBPos : Vector3;
var key : boolean = false;
var flag : boolean = false;
private var speed : float;
var waitFor : int;

function Start () {
	startPos = transform.position;
	targetAPos = targetA.transform.position;
	targetBPos = targetB.transform.position;
	speed = .02;
}

function Update () {
	if(key){
		if(flag)
			MoveToA();
		else
			MoveToB();
	}
}

function MoveToA(){
	transform.position = Vector3.Lerp(transform.position, targetAPos, speed);
	yield WaitForSeconds(waitFor);
	flag = false;
}

function MoveToB(){
	transform.position = Vector3.Lerp(transform.position, targetBPos, speed);
	yield WaitForSeconds(waitFor);
	flag = true;
}