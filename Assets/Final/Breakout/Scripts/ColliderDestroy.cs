using UnityEngine;
using System.Collections;

public class ColliderDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnCollisionEnter (Collision other) {
		if(other.gameObject.tag == "evil"||other.gameObject.tag == "heal") 
		{
			Destroy(other.gameObject);
		}	
		if (other.gameObject.tag == "Ball") 
		{
			ScoreBoard.score-=4;
			Destroy(other.gameObject);
			
		}
		if (other.gameObject.tag == "grabBall") 
		{
			//ScoreBoard.score-=7;

			
		}
	


	}
}
