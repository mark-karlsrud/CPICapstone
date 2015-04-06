//Other Player Variables go here
var keysCollected : int;
var jumpHeight : float;
var moveSpeed : float;
private var player : GameObject;
private var charMotor : CharacterMotor;
function Start(){
	player = GameObject.FindGameObjectWithTag("Player");
	charMotor = player.GetComponent(CharacterMotor);
	keysCollected = 0;
	jumpHeight = 1;
	moveSpeed = 6;
	
}

function Update(){
	UpdateJump();
	UpdateMove();
}

function UpdateJump (){
	charMotor.jumping.baseHeight = jumpHeight;
}

function UpdateMove(){
	charMotor.movement.maxForwardSpeed = moveSpeed;
	charMotor.movement.maxBackwardsSpeed = moveSpeed;
	charMotor.movement.maxSidewaysSpeed = moveSpeed;
}