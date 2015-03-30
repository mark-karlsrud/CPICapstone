using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {
	public string scene;
	public float startTime;
	public float waitTime;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > waitTime) {
			Application.LoadLevel(scene);
		}
	}
}
