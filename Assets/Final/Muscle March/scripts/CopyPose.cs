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
        transform.Translate(transform.forward*Time.deltaTime);
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
        else if (m.RightBicepFlexDown())
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("right bicep flex down"))
                animator.SetTrigger("right bicep flex down");
        }
        else if (m.LeftBicepFlexDown())
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("left bicep flex down"))
                animator.SetTrigger("left bicep flex down");
        }
        else
        {
            /*
            if (!(animator.GetCurrentAnimatorStateInfo(3).IsName("arm inside") ||
                    animator.GetCurrentAnimatorStateInfo(3).IsName("arm outside") ||
                    animator.GetCurrentAnimatorStateInfo(3).IsName("arm falling")))
                animator.SetTrigger("no pose");
            */
        }
	}
}
