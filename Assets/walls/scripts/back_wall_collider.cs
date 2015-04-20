using UnityEngine;
using System.Collections;

public class back_wall_collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		//resets animation
		if (other.gameObject.tag == "muscleENEMY" ||
						other.gameObject.tag == "muscleAI1" ||
						other.gameObject.tag == "muscleAI2" || 
						other.gameObject.tag == "muscleAI3")
			other.gameObject.GetComponent<Animator> ().SetTrigger ("no pose");
	}
}
