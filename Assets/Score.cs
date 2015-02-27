using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	 public int score = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision other) {
		if (other.gameObject.tag == "Ball") {
			score -= 50;
			Destroy(other.gameObject);
		} else if (other.gameObject.tag == "evil") {
			Destroy(other.gameObject);
		} else if (other.gameObject.tag == "heal") {
			Destroy(other.gameObject);
		}

			
		}
}

