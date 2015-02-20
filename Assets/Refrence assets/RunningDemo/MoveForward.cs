using UnityEngine;
using System.Collections;

public class MoveForward : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		player.transform.Translate (Vector3.up*Time.deltaTime);
	
	}
}
