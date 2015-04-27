﻿using UnityEngine;
using System.Collections;

public class SetWalls : MonoBehaviour {

    public GameObject[] wallPossibilities;

	// Use this for initialization
	void Start () {
        GameObject wallParent = GameObject.Find("Walls");
        foreach (Transform wall in wallParent.transform)
        {
            if (wall.gameObject.tag == "wall")
            {
                GameObject newObject;
                newObject = wallPossibilities[Random.Range(0, wallPossibilities.Length)];
                newObject.transform.position = wall.transform.position;
                newObject.transform.rotation = wall.transform.rotation;
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
