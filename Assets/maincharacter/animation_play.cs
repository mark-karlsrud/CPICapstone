using UnityEngine;
using System.Collections;

public class animation_play : MonoBehaviour {

	Animator animator;

	//movement hashes
	int jumpHash = Animator.StringToHash("Jump");
	int runForwardHash = Animator.StringToHash("Run Forwards");
	int runBackwardHash = Animator.StringToHash("Run Backwards");
	int sideStepRightHash = Animator.StringToHash("Side Step Right");
	int sideStepLefttHash = Animator.StringToHash("Side Step Left");

	//interact hashes
	int buttonPressHash = Animator.StringToHash("Button Press");
	int pickupHash = Animator.StringToHash("Pickup");

	//attack hashes
	int punchinHash = Animator.StringToHash("Punchin");
	int leftPunchHash = Animator.StringToHash("Left Punch");
	int rightPunchHash = Animator.StringToHash("Right Punch");
	int shootinHash = Animator.StringToHash("Shootin");
	int firingHash = Animator.StringToHash("Firing");

	//attack vaiables
	bool isPunchin;
	bool isShootin;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		isPunchin = false;
		isShootin = false;
	}
	
	// Update is called once per frame
	void Update () {

		animator.SetBool (runForwardHash, Input.GetKey(KeyCode.W));
		animator.SetBool (runBackwardHash, Input.GetKey(KeyCode.S));
		animator.SetBool (sideStepRightHash, Input.GetKey(KeyCode.D));
		animator.SetBool (sideStepLefttHash, Input.GetKey(KeyCode.A));

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			//doesn't block punch animations when pushing just so you know :/
			if(!animator.GetCurrentAnimatorStateInfo(2).IsName("button press"))
			{
				animator.SetTrigger(buttonPressHash);
			}
		}

		if (Input.GetKeyDown (KeyCode.E)) 
		{
			//does block every other animation
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("pickup"))
			{
				animator.SetTrigger(pickupHash);
			}
		}

		if (Input.GetAxis ("Jump") != 0) 
		{
		
		if(!(animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Left") ||
		   animator.GetCurrentAnimatorStateInfo(0).IsName("Jump Right")))
			{
				animator.SetTrigger(jumpHash);

				//****doesn't reactivate******//
				//deactivates punching when jumping
				isPunchin = false;
				punchState();

				//deactuvates shooting
				isShootin = false;
				shootState();
			}
		}

		if (Input.GetKeyDown (KeyCode.Keypad1)) 
		{
			//deactivates shootings
			isShootin = false;
			shootState();
			
			isPunchin = !isPunchin;
			animator.SetBool (punchinHash, isPunchin);

		}

		if (Input.GetKeyDown (KeyCode.Keypad2)) 
		{
			//deactivates punching
			isPunchin = false;
			punchState();
			
			isShootin = !isShootin;	
			animator.SetBool (shootinHash, isShootin);
		}

		if (Input.GetAxis ("Fire1") != 0) 
		{
			if(isPunchin)
			{
				animator.SetBool(leftPunchHash, true);
			}
			else if(isShootin)
			{
				animator.SetBool(firingHash, true);
			}
		}
		else
		{
			animator.SetBool(leftPunchHash, false);
			animator.SetBool(firingHash, false);
		}


		if (Input.GetAxis ("Fire2") != 0) 
		{
			if (isPunchin) 
			{
				animator.SetBool (rightPunchHash, true);
			}
		} 
		else 
		{
			animator.SetBool (rightPunchHash, false);
		}


		if (!isPunchin) 
		{
			animator.SetBool (leftPunchHash, false);
			animator.SetBool (rightPunchHash, false);
		}

		if (!isShootin) 
		{
			animator.SetBool(firingHash, false);
		}
	}

	void punchState()
	{
		//deactivates or activates the punching animations
		if (!isPunchin) 
		{
			animator.SetBool (leftPunchHash, false);
			animator.SetBool (rightPunchHash, false);
			animator.SetBool (punchinHash, isPunchin);
		}
		else
			animator.SetBool (punchinHash, isPunchin);	
	}

	void shootState()
	{
		//deactivatess or activates the shooting animations
		if (!isShootin) 
		{
			animator.SetBool (firingHash, false);	
			animator.SetBool (shootinHash, isShootin);
		}
		else
			animator.SetBool (shootinHash, isShootin);
	}
}
