using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	public bool destroy;
	Vector3 position;

	// Use this for initialization
	void Start () {
		position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerScript>().spawn ++;
			gameObject.collider.enabled = false;
			Destroy(gameObject);
		}
	}
}
