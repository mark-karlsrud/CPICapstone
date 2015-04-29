using UnityEngine;
using System.Collections;

public class mid_wall_collider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        print(Controller.muscleHealth);
	}

	void OnTriggerEnter (Collider other)
	{
		//player looses one health
		if (other.gameObject.tag == "Player")
		{
			if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
   					IsName ("left up right down bicep flex")) &&
                    this.transform.parent.gameObject.name == "bicep flex left up and right down(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
        			IsName ("right up left down bicep flex")) &&
                    this.transform.parent.gameObject.name == "bicep flex right up and left down(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
         			IsName ("double bicep flex")) &&
                    this.transform.parent.gameObject.name == "double bicept flex(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			         IsName ("double bicep flex down")) &&
                     this.transform.parent.gameObject.name == "double bicept flex down(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			         IsName ("left bicep flex")) &&
                     this.transform.parent.gameObject.name == "left bicept flex(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			          IsName ("right bicep flex")) &&
                     this.transform.parent.gameObject.name == "right bicept flex(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			          IsName ("left bicep flex down")) &&
                     this.transform.parent.gameObject.name == "left bicept flex down(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			          IsName ("right bicep flex down")) &&
                     this.transform.parent.gameObject.name == "right bicept flex down(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			          IsName ("touchdown")) &&
                     this.transform.parent.gameObject.name == "touchdown(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			          IsName ("touchdown down")) &&
                     this.transform.parent.gameObject.name == "touchdown down(Clone)")
				Controller.muscleHealth--;

			else if ((!other.gameObject.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (3).
			          IsName ("tpose")) &&
                     this.transform.parent.gameObject.name == "tpose(Clone)")
				Controller.muscleHealth--;
            else
            {
                other.gameObject.GetComponent<PointScript>().rightPose();
            }

		}



		//kills dead AI
		else if (other.gameObject.tag == "muscleAI1" && Controller.muscleAI1Dead)
			Destroy(other.gameObject);
		else if (other.gameObject.tag == "muscleAI2" && Controller.muscleAI2Dead)
			Destroy(other.gameObject);
		else if (other.gameObject.tag == "muscleAI3" && Controller.muscleAI3Dead)
			Destroy(other.gameObject);
	}
}
