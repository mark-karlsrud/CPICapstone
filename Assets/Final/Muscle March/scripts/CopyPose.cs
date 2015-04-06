using UnityEngine;
using System.Collections;

public class CopyPose : MonoBehaviour {

	public Camera c;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MuscleGestureListener m = c.GetComponent<MuscleGestureListener> ();
		if (m.IsTPose()) {
			this.animation.Play ("tpose start");
		}
	}
}
