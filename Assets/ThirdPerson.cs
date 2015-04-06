using UnityEngine;
using System.Collections;

public class ThirdPerson : MonoBehaviour {

	public  float distanceAway;
	public  float distanceUp;
	public  float smooth;
	public  Transform followXForm;
	public Vector3 targetPosition;
	private Vector3 offset = new Vector3(0f,1.5f,0f);
	private Vector3 lookDir;
	private Vector3 velocityCanSmooth = Vector3.zero;
	private float canSmoothDampTime = 0.1f;


	// Use this for initialization
	void Start () 
	{
		followXForm = GameObject.FindWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	// Happens after update 
	void LateUpdate()
	{
		//Calculate distance from the follow transform plus so height 
		Vector3 characterOffset = followXForm.position + offset;


		//Calculate the direction the camera is in relation to the player
		lookDir = characterOffset - this.transform.position;
		lookDir.y = 0;
		lookDir.Normalize ();
		Debug.DrawRay (this.transform.position, lookDir, Color.yellow);

		//find the position move the camera up and then move the camera back 
		targetPosition = characterOffset + followXForm.up * distanceUp - lookDir * distanceAway;
		//Drawing the Characters Up Vector 
		Debug.DrawRay (followXForm.position, followXForm.up * distanceUp, Color.red);
		//Draw the forward vector 
		Debug.DrawRay (followXForm.position, -1f*followXForm.forward * distanceAway, Color.blue);
		// Draw a line from character to target postion
		Debug.DrawLine (followXForm.position, targetPosition, Color.green);

		CompensateForWalls (characterOffset, ref targetPosition);

		//Go between your position to the target position smoothly 

		
		smoothPosition(this.transform.position, targetPosition);

		//Make sure we are always facing the character 
		transform.LookAt (followXForm);




	}

	private void smoothPosition (Vector3 fromPos, Vector3 toPos)
	{
			//Making smoth transition between cameras current position and the position it wants to be in 
		this.transform.position = Vector3.SmoothDamp(fromPos, toPos, ref velocityCanSmooth, canSmoothDampTime);
	}
														//Acessing totarget by refrence allows us to change this value and 
	private void CompensateForWalls(Vector3 fromObject, ref Vector3 toTarget)
	{
		//Draw a line from the object to the target 
		Debug.DrawLine (fromObject, toTarget, Color.cyan);

		//Make a raycast hit value that is going to be when it hit the wall 
		RaycastHit wallHit = new RaycastHit();

		//If you do hit the wall then shit the target a little bit 
		if(Physics.Linecast(fromObject , toTarget, out wallHit))
	    {
			//draw a vector where we hit 
			Debug.DrawRay(wallHit.point, Vector3.left,Color.cyan);

			//Move camera to X and Z hit points while keeping the y value the same
			//toTarget = new Vector3 (wallHit.point.x , toTarget.y, wallHit.point.z);
		}

	}
}
