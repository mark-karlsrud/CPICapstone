using UnityEngine;
using System.Collections;

public class Trigger : MonoBehaviour {
	public GameObject icosphere;
	public GameObject icoCollider;
	public bool didCollide;

	// Update is called once per frame
	void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Player") {
			//DestroyObject(icoCollider);
			icosphere.collider.enabled = true;
			didCollide = true;
			
		}
	}
}
