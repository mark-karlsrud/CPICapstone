#pragma strict
var smooth: float = 2;
var newPosition:Vector3;
var target:GameObject;

function Awake(){
	newPosition = transform.position;	
}
function Start(){
	var positionA:Vector3 = target.transform.position;
	newPosition = positionA;
}

function Update(){
	ChangePosition();
}

function ChangePosition(){
	transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * smooth);
}
