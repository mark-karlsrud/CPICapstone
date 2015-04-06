using UnityEngine;
using System.Collections;

public class CopyPose : MonoBehaviour {

	public Camera c;
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		MuscleGestureListener m = c.GetComponent<MuscleGestureListener> ();
		if (m.IsTPose()) {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("tpose"))
                animator.SetTrigger("tpose");
		}
        else if (m.RightBicepFlex())
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("right bicep flex"))
                animator.SetTrigger("right bicep flex");
        }
        else if (m.LeftBicepFlex())
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("left bicep flex"))
                animator.SetTrigger("left bicep flex");
        }
	}
}
