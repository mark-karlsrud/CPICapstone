using UnityEngine;
using System.Collections;

public class SphereSmarts : MonoBehaviour {
	public GameObject player;
	public GameObject icoCollider;
	public bool didCollide, run ,start, reverse;
	
	
	void Start()
	{
		start = true;
		reverse = false;
		rigidbody.velocity = Vector3.left * 10;
		
	}

	
	void Update()
	{
		if (reverse) 
		{
			//rigidbody.velocity = Vector3.up * 10;
		}
		
		if (run) {
			run = true;
			//transform.Translate (new Vector3 (1, 0, 0)*Time.deltaTime);	
		}
	}
	

}
