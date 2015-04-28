using UnityEngine;
using System.Collections;

public class SetWalls : MonoBehaviour {

    public GameObject[] wallPossibilities;

	// Use this for initialization
	void Start () {
        GameObject railParent = GameObject.Find("Rail");
        foreach (Transform wall in railParent.transform)
        {
            wall.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        GameObject wallParent = GameObject.Find("Walls");
        foreach (Transform wall in wallParent.transform)
        {
            if (wall.gameObject.tag == "wall")
            {
                GameObject newObject;
                newObject = wallPossibilities[Random.Range(0, wallPossibilities.Length)];
                newObject.transform.position = wall.transform.position;
                newObject.transform.rotation = wall.transform.rotation;
                if (newObject.name.Contains("right"))
                    newObject.transform.Rotate(new Vector3(0,0,180));
                newObject.transform.localScale = wall.transform.localScale;
                newObject.tag = "wall";
                Instantiate(newObject);
            }
        }
        Destroy(wallParent);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
