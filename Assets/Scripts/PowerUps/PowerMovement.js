#pragma strict
private var rotateVector : Vector3 = Vector3.up;
private var rotateSpeed : float = 50;
private var startPos : Vector3;
private var endPos : Vector3;
private var speed : float = 2;
private var flag : boolean = false;
private var player : GameObject;

function Start () {
	speed = Time.deltaTime * 3;
	rotateVector *= rotateSpeed;
	startPos = transform.position;
	endPos = startPos + Vector3(0,.5,0);
	player = GameObject.FindGameObjectWithTag("Player");
}

function Update () {
	transform.Rotate(rotateVector * Time.deltaTime);
	if(flag)
		HoverUp();
	else
		HoverDown();
}

function HoverUp () {
	transform.position = Vector3.Lerp(transform.position, endPos, speed);
	yield WaitForSeconds(1);
	flag = false;
}
function HoverDown () {
	transform.position = Vector3.Lerp(transform.position, startPos, speed);
	yield WaitForSeconds(1);
	flag = true;
}

function OnTriggerEnter(col : Collider){
	if(col == player.collider){
		var gameManager = GameObject.Find("GameManager");
		UnityEngine.Object.Destroy(gameObject);
		gameManager.GetComponent(VariableManager).jumpHeight += 4;
	}
}
