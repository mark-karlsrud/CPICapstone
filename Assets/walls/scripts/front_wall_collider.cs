using UnityEngine;
using System.Collections;

public class front_wall_collider : MonoBehaviour {

	int randomCollide;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other)
	{
        
		if (other.gameObject.tag == "isTriggerBox") 
		{
			if (Controller.muscleWallCount > 2) //garrentied 3 walls before a hit
			{  
				randomCollide = Random.Range (1, 6); // 1 in five chance to hit

				if (randomCollide <= (Controller.muscleWallCount - 2)) //increases the chances by 
				{ 													 //one for every wall after 3
													
					Controller.muscleWallCount = -1;//resets the count to zero... this wall adds one hence -1
					
					//checks which character isn't dead and makes it dead

					if (!Controller.muscleAI3Dead)
							Controller.muscleAI3Dead = true;
					else if (!Controller.muscleAI2Dead)
							Controller.muscleAI2Dead = true;
					else if (!Controller.muscleAI1Dead)
							Controller.muscleAI1Dead = true;
				}
			}

			Controller.muscleWallCount++;
		} 
		//sets animation to falling if dead
		else if (other.gameObject.tag == "muscleAI1" && Controller.muscleAI1Dead)
				other.gameObject.GetComponent<Animator> ().SetTrigger ("falling");
		else if (other.gameObject.tag == "muscleAI2" && Controller.muscleAI2Dead)
				other.gameObject.GetComponent<Animator> ().SetTrigger ("falling");
		else if (other.gameObject.tag == "muscleAI3" && Controller.muscleAI3Dead)
				other.gameObject.GetComponent<Animator> ().SetTrigger ("falling");

		//sets everyone else to pose
		else if (other.gameObject.tag == "muscleENEMY" || other.gameObject.tag == "muscleAI1" || 
				other.gameObject.tag == "muscleAI2" || other.gameObject.tag == "muscleAI3")
		{
            if (this.transform.parent.gameObject.name == "bicep flex left up and right down(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("left up right down bicep flex");

            else if (this.transform.parent.gameObject.name == "bicep flex right up and left down(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("right up left down bicep flex");

            else if (this.transform.parent.gameObject.name == "double bicept flex(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("double bicep flex");

            else if (this.transform.parent.gameObject.name == "double bicept flex down(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("double bicep flex down");

            else if (this.transform.parent.gameObject.name == "left bicept flex(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("left bicep flex");

            else if (this.transform.parent.gameObject.name == "right bicept flex(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("right bicep flex");

            else if (this.transform.parent.gameObject.name == "left bicept flex down(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("left bicep flex down");

            else if (this.transform.parent.gameObject.name == "right bicept flex down(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("right bicep flex down");

            else if (this.transform.parent.gameObject.name == "touchdown(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("touchdown");

            else if (this.transform.parent.gameObject.name == "touchdown down(Clone)")
				other.gameObject.GetComponent<Animator> ().SetTrigger ("touchdown down");
         
			else if (this.transform.parent.gameObject.name == "tpose(Clone)")
                other.gameObject.GetComponent<Animator>().SetTrigger("tpose");
		}

	}
}
