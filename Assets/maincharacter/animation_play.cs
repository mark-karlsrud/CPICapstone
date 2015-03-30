using UnityEngine;
using System.Collections;

public class animation_play : MonoBehaviour {

	Animator animator;

	//movement hashes
	int jumpHash = Animator.StringToHash("Jump");
	int movingHash = Animator.StringToHash("Moving");

	//attack hashes
	int meleeHash = Animator.StringToHash("Melee");
	int leftPunchHash = Animator.StringToHash("Punch Left");
	int rightPunchHash = Animator.StringToHash("Punch Right");
	int aimHash = Animator.StringToHash("Aim");
	int firingHash = Animator.StringToHash("Firing");

	//attack vaiables
	bool isMelee;
	bool isAttackEnd;
	bool isAim;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		isMelee = false;
		isAttackEnd = false;
		isAim = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Vertical") != 0) 
		{
			animator.SetBool (movingHash, true);
		} 
		else 
		{
			animator.SetBool (movingHash, false);
		}


		if (Input.GetAxis ("Jump") != 0) 
		{
				animator.SetTrigger(jumpHash);
				isMelee = false;
				animator.SetBool (meleeHash, isMelee);
				isAim = false;		
				animator.SetBool (aimHash, isAim);
		}

		if (Input.GetKeyDown (KeyCode.Keypad1)) 
		{
			isMelee = !isMelee;		
			animator.SetBool (meleeHash, isMelee);
		}

		if (Input.GetKeyDown (KeyCode.Keypad2)) 
		{
			isAim = !isAim;		
			animator.SetBool (aimHash, isAim);
		}

		if (Input.GetAxis ("Fire1") != 0) 
		{
			if(isMelee)
			{
				animator.SetTrigger(leftPunchHash);
			}
			else if(isAim)
			{
				animator.SetBool(firingHash, true);
			}
		}
		else
		{
			animator.SetBool(firingHash, false);
		}

		if (Input.GetAxis ("Fire2") != 0) 
		{
			if(isMelee)
			{
				animator.SetTrigger(rightPunchHash);
			}
		}

		if (isMelee || isAim) 
		{
			animator.SetLayerWeight (1, 1);
		}
		else if (animator.GetCurrentAnimatorStateInfo(1).IsName("attack.punch start") ||
		         animator.GetCurrentAnimatorStateInfo(1).IsName("attack.shoot start"))
		{
			isAttackEnd = true;
		}
		else if (isAttackEnd) 
		{
			isAttackEnd = false;
			animator.SetLayerWeight(1,0);
		}
	}
}
