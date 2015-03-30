using UnityEngine;
using System.Collections;

public class animation_enemy: MonoBehaviour {

	Animator animator;
	
	int gunsActiveHash = Animator.StringToHash("guns active");
	bool isActive;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
		isActive = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.Q)) 
		{
			isActive = !isActive;
			animator.SetBool (gunsActiveHash, isActive);
		}
	}
}
