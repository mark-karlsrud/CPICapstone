using UnityEngine;
using System.Collections;

public class animation_bowser: MonoBehaviour {

	Animator animator;
	
	int throwHash = Animator.StringToHash("Throw");

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)) 
		{
			if(animator.GetCurrentAnimatorStateInfo(0).IsName("no animation"))
				animator.SetTrigger(throwHash);
		}
	}
}
