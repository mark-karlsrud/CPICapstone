using UnityEngine;
using System.Collections;
using System;

public class SphereSmarts : MonoBehaviour {
	public GameObject player;
	public GameObject icoCollider;
	public bool didCollide, run ,start, reverse;
	private Score score;
	public float moveForce;
	public float jumpFOrce;
	public float speed = 10;





	
	void Start()
	{
		start = true;
		reverse = false;
		rigidbody.velocity = Vector3.left *speed; //10
	

		
	}

	
	void Update()
	{
		//Debug.Log (transform.rigidbody.velocity);
		if (Mathf.Abs(transform.rigidbody.velocity.x) < 5.0f && Mathf.Abs(transform.rigidbody.velocity.y) < 5.0f)
		{
			if(Mathf.Abs(transform.rigidbody.velocity.x) == 0.0f && Mathf.Abs(transform.rigidbody.velocity.y) == 0.0f)
			{
				transform.rigidbody.velocity = new Vector3(5f,5f,0);
			}
			else{
				transform.rigidbody.velocity *= 1.1f;
			}
			/*
			if(transform.rigidbody.velocity.x > transform.rigidbody.velocity.y){
				transform.rigidbody.velocity =new Vector3(5,transform.rigidbody.velocity.y,0);
			}
			else{
				transform.rigidbody.velocity =new Vector3(transform.rigidbody.velocity.x,5,0);
			}*/
		}
	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Player") {
			other.gameObject.rigidbody.constraints = RigidbodyConstraints.None;
			ScoreBoard.score+=5;
			//Debug.Log(ScoreBoard.score);
			Destroy(gameObject.collider);
			rigidbody.velocity = Vector3.right * 10;


		}


	}
}
