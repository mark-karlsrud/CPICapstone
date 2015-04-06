#pragma strict

var testObj : GameObject;

private var chMotor : CharacterMotor;

private var superSprint : boolean = false;
private var superJump : boolean = false;

private var canHover : boolean = false;

function Start()
{
	chMotor = GetComponent (CharacterMotor);
}

function Update()
{
var fwd = transform.TransformDirection(Vector3.forward);
var hit : RaycastHit;

if(Physics.Raycast(transform.position, fwd, hit))
{
	if(hit.distance <= 5.0 && hit.collider.gameObject.tag == "pickup")
	{
	canHover = true;
	
	if(Input.GetKeyDown("e"))
	{
	Destroy(testObj);
	superJump = !superJump;
		
		if(superJump == true)
		{
			chMotor.movement.maxForwardSpeed = 50;
			chMotor.movement.maxSidewaysSpeed = 50;
		}
		
		else if(superJump == false)
		{
			chMotor.jumping.baseHeight = 5;
			chMotor.movement.maxFallSpeed = 10;
		}	
	}
	}
	
	else if(hit.distance <= 5.0 && hit.collider.gameObject.tag == "pickupSprint")
	{
		canHover = true;
		
		if(Input.GetKeyDown("e"))
	{
	Destroy(testObj);
	superJump = !superJump;
		
		if(superSprint == true)
		{
			chMotor.jumping.baseHeight = 10;
			chMotor.movement.maxFallSpeed = 20;
		}
		
		else if(superSprint == false)
		{
			chMotor.movement.maxForwardSpeed = 6;
			chMotor.movement.maxSidewaysSpeed = 6; 
		}	
	}
	}
	else
	{
	canHover = false;
	}
	}
	}
	