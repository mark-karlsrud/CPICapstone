using UnityEngine;
using System.Collections;

public class Transition : MonoBehaviour {

	public GameObject player,tube;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "Player") {
			Debug.Log("thje");

		}
	}
}