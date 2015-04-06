/*
using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {
	public LayerMask unwalkable;
	public Vector3 gridWorldSize;
	public float nodeRadius;
	Node[,,] grid;
	float nodeDiameter;
	int gridSizeX, gridSizeY, gridSizeZ;



	void Start()
	{
		nodeDiameter = nodeRadius * 2;
		//Returns how many nodes we can fit in side the world size x 
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
		gridSizeZ = Mathf.RoundToInt(gridWorldSize.z / nodeDiameter);
		CreateGrid ();
	

		}

	void CreateGrid()
	{
	   grid = new Node[gridSizeX, gridSizeY, gridSizeZ];
		//Findin the bottom left of our world. First find left edge then find the bottom left corner 
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.z / 2*Vector3.up * gridWorldSize.y / 2;

		//Loop through all positions the nodes will be in to do collision checks to se if they are walkable or not 
		for (int x =0; x<gridSizeX; x++)
						for (int y =0; x<gridSizeY; y++)
								for (int z =0; z<gridSizeZ; z++) 
									{ Vector3 worldpoint = worldBottomLeft+ Vector3.right*(x*nodeDiameter +nodeRadius)+ Vector3.up*(y * nodeDiameter + nodeRadius) + Vector3.forward*(z *nodeDiameter+nodeRadius);
									  bool walkable = !(Physics.CheckSphere(worldpoint,nodeRadius,unwal));
									  grid[x,y,z] = new Node(walkable,worldpoint);

									
									}
	
	}


	void OnDrawGizmos()
	{

		Gizmos.DrawWireCube (transform.position, new Vector3 (gridWorldSize.x, gridWorldSize.z, gridWorldSize.y));

		if (grid != null)
			foreach(Node n in grid)
		{
			Gizmos.color= Color.white;
			Gizmos.DrawSphere(n.worldPosition,Vector3.one *(nodeDiameter- .1f));
		}

	
	}
}
*/