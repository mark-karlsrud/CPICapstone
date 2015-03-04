﻿using UnityEngine;
using System.Collections;

public class HealBall : MonoBehaviour {
	
	public GameObject player;
	private float playerY;
	
	// Use this for initialization
	void Start () 
	{
		
		rigidbody.velocity = Vector3.left * 15;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerY = player.transform.localScale.y;
	}
	
	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Player") {
			player.transform.localScale = new Vector3(1,playerY+1,1);
			
		}
	}
	
}