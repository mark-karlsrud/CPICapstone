using UnityEngine;
using System.Collections;

public class animationtest : MonoBehaviour {
	
	public string[] testname;

    public int index;

	// Use this for initialization
	void Start () {
		index = 1;
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.anyKeyDown) 
		{
			this.animation.Play (testname [index]);
			index = index + 1;
			if (index % testname.Length == 0)
				index = 1;
		}
	}
}
