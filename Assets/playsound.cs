using UnityEngine;
using System.Collections;
using System;
using System.Runtime.CompilerServices;

public class playsound : MonoBehaviour {

	public AudioClip powerDown,powerUp;
	// Use this for initialization

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "evil") {
		
			audio.PlayOneShot(powerDown);

		}
		if(other.gameObject.tag == "heal") {
			
			audio.PlayOneShot(powerUp);
			
		}
	}

	
}


