using UnityEngine;
using System.Collections;

public class Node {
	
	public bool walkable;
	public Vector3 worldPosition;

	//Why write this constructor ? why not just assign the 
	public Node (bool walkable, Vector3 worldPos)
	{
		walkable = walkable;
		worldPos = worldPosition;

	}



}
