using UnityEngine;
using System.Collections;

public class TestCube1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.forward * Time.deltaTime);
	}

    void OnCollisionEnter(Collision col){
        Debug.Log("collided");/*
        if (col.gameObject.tag == "destination")
        {
            Destroy(col.gameObject);
        }*/
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("collided");/*
        Debug.Log(col.gameObject.tag);

        if (col.gameObject.tag == "destination")
        {
            target = walls.Dequeue();
            Destroy(col.gameObject);
        }*/
    }
}
