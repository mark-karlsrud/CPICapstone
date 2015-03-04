using UnityEngine;
using System.Collections;

public class SphereSmarts : MonoBehaviour {
	public GameObject player;
	public GameObject icoCollider;
	public bool didCollide, run ,start, reverse;
	private Score score;
	
	void Start()
	{
		start = true;
		reverse = false;
		rigidbody.velocity = Vector3.left * 10;
		
	}

	
	void Update()
	{

	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Player") {
			score.score+=50;
			
		}
	}
}
