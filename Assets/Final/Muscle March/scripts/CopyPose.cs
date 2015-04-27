using UnityEngine;
using System.Collections;
using System.Timers;

public class CopyPose : MonoBehaviour {

	public Camera c;
    Animator animator;
    private Timer noPoseTimer;
    private bool noPose;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        noPoseTimer = new System.Timers.Timer();
        noPoseTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Translate(5*transform.forward*Time.deltaTime);
		MuscleGestureListener m = c.GetComponent<MuscleGestureListener> ();
        
		if (m.IsTPose())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("tpose"))
                animator.SetTrigger("tpose");
		}
        else if (m.DoubleBicepFlex())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("double bicep flex"))
                animator.SetTrigger("double bicep flex");
        }
        else if (m.RightBicepFlex())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("right bicep flex"))
                animator.SetTrigger("right bicep flex");
        }
        else if (m.LeftBicepFlex())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("left bicep flex"))
                animator.SetTrigger("left bicep flex");
        }
        else if (m.DoubleBicepFlexDown())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("double bicep flex down"))
                animator.SetTrigger("double bicep flex down");
        }
        else if (m.RightBicepFlexDown())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("right bicep flex down"))
                animator.SetTrigger("right bicep flex down");
        }
        else if (m.LeftBicepFlexDown())
        {
            noPoseTimer.Enabled = false;
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("left bicep flex down"))
                animator.SetTrigger("left bicep flex down");
        }
        
        else //if not pose was detected in the past 2 seconds, assume the no pose pose
        {
            /*
            if (!(animator.GetCurrentAnimatorStateInfo(3).IsName("arm inside") ||
                    animator.GetCurrentAnimatorStateInfo(3).IsName("arm outside") ||
                    animator.GetCurrentAnimatorStateInfo(3).IsName("arm falling")))
                animator.SetTrigger("no pose");
            */
            if (noPose)
            {
                Debug.Log("NO POSE");
                if (!animator.GetCurrentAnimatorStateInfo(3).IsName("no pose"))
                    animator.SetTrigger("no pose");
                noPose = false;
            }
            if (!noPoseTimer.Enabled)
            {
                noPoseTimer.Interval = 2300; //3 seconds
                noPoseTimer.Enabled = true;
                //Debug.Log("start timer");
                //poseTimer.Start();
            }
        }

        /*THIS IS FOR TESTING PURPOSES*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!animator.GetCurrentAnimatorStateInfo(3).IsName("tpose"))
                animator.SetTrigger("tpose");
        }
	}

    private void OnTimedEvent(object sender, ElapsedEventArgs e)
    {
        //Debug.Log("timed event");
        noPose = true;
        noPoseTimer.Enabled = false;
    }
}
