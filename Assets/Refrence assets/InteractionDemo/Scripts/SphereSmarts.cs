using UnityEngine;
using System.Collections;

public class SphereSmarts : MonoBehaviour {
	public GameObject player;
	public GameObject icoCollider;
	public bool didCollide, run ,start;
	
	
	void Start()
	{
		start = true;
		rigidbody.velocity = Vector3.right * 5;
		
	}
	// Update is called once per frame
	
	void Update()
	{
		if (start) {
			//transform.Translate (new Vector3 (-1, 0, 0)*Time.deltaTime);	
		}
		
		if (run) {
			run = true;
			//transform.Translate (new Vector3 (1, 0, 0)*Time.deltaTime);	
		}
	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Player") {
			didCollide = true;
			run=true;
		}
	}
}
