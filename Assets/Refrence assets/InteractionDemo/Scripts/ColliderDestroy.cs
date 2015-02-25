using UnityEngine;
using System.Collections;

public class ColliderDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Ball") {
			other.gameObject.collider.enabled=false;
}
	}
}
