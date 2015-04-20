using UnityEngine;
using System.Collections;

public class particle_Tigger_collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
		//enemy breaks the wall ... deletion of the wall is on the full wall
		if (other.gameObject.tag == "muscleENEMY")
			gameObject.GetComponent<ParticleSystem> ().Play();
	}
}
