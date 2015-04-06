using UnityEngine;
using System.Collections;

public class animation_test: MonoBehaviour {

	Animator animator;

	//arm movement hashes
	int insideHash = Animator.StringToHash("inside");
	int fallingHash = Animator.StringToHash("falling");

	//arm varials
	bool isInside;

	//pose hashes
	int noPoseHash = Animator.StringToHash ("no pose");
	int doubleBicepFlexDownHash = Animator.StringToHash("double bicep flex down");
	int doubleBicepFlexHash = Animator.StringToHash("double bicep flex");
	int leftBicepFlexDownHash = Animator.StringToHash("left bicep flex down");
	int leftBicepFlexHash = Animator.StringToHash("left bicep flex");
	int leftUpRightDownBicepFlexHash = Animator.StringToHash("left up right down bicep flex");
	int rightBicepFlexDownHash = Animator.StringToHash("right bicep flex down");
	int rightBicepFlexHash = Animator.StringToHash("right bicep flex");
	int rightUpLeftDownBicepFlexHash = Animator.StringToHash("right up left down bicep flex");
	int touchdownDownHash = Animator.StringToHash("touchdown down");
	int touchdownHash = Animator.StringToHash("touchdown");
	int tposeHash = Animator.StringToHash("tpose");

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		isInside = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.A)) 
		{
			isInside = !isInside;
			animator.SetBool (insideHash, isInside);
		}

		if (Input.GetKeyDown (KeyCode.Q)) 
		{
			animator.SetTrigger(fallingHash);
		}



		//poses
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			if(!(animator.GetCurrentAnimatorStateInfo(3).IsName("arm inside")||
			     animator.GetCurrentAnimatorStateInfo(3).IsName("arm outside")||
			     animator.GetCurrentAnimatorStateInfo(3).IsName("arm falling")))
				animator.SetTrigger(noPoseHash);
		}
		
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("double bicep flex down"))
				animator.SetTrigger(doubleBicepFlexDownHash);
		}

		if (Input.GetKeyDown (KeyCode.R)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("double bicep flex"))
				animator.SetTrigger(doubleBicepFlexHash);
		}

		if (Input.GetKeyDown (KeyCode.T)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("left bicep flex down"))
				animator.SetTrigger(leftBicepFlexDownHash);
		}

		if (Input.GetKeyDown (KeyCode.Y)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("left bicep flex"))
				animator.SetTrigger(leftBicepFlexHash);
		}

		if (Input.GetKeyDown (KeyCode.U)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("left up right down bicep flex"))
				animator.SetTrigger(leftUpRightDownBicepFlexHash);
		}

		if (Input.GetKeyDown (KeyCode.I)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("right bicep flex down"))
				animator.SetTrigger(rightBicepFlexDownHash);
		}

		if (Input.GetKeyDown (KeyCode.O)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("right bicep flex"))
				animator.SetTrigger(rightBicepFlexHash);
		}

		if (Input.GetKeyDown (KeyCode.P)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("right up left down bicep flex"))
				animator.SetTrigger(rightUpLeftDownBicepFlexHash);
		}

		if (Input.GetKeyDown (KeyCode.L)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("touchdown down"))
				animator.SetTrigger(touchdownDownHash);
		}

		if (Input.GetKeyDown (KeyCode.K)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("touchdown"))
				animator.SetTrigger(touchdownHash);
		}

		if (Input.GetKeyDown (KeyCode.J)) 
		{
			if(!animator.GetCurrentAnimatorStateInfo(3).IsName("tpose"))
				animator.SetTrigger(tposeHash);
		}
	}
}
