using UnityEngine;
using System.Collections;

public class freezeX : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
		rigidbody.constraints = RigidbodyConstraints.FreezeAll;

	}
}
