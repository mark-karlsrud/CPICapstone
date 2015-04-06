using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
	
	public GameObject player;

	public Transform SpawnPoint;
	public Transform CheckPoint1;
	public Transform CheckPoint2;
	
	public Vector3 sp, cp1, cp2;
	


	// Use this for initialization
	void Start () {
		sp = SpawnPoint.position;
		cp1 = CheckPoint1.position;
		cp2 = CheckPoint2.position;
	}
	
	// Update is called once per frame
	void Update () {
	}

	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Player")
		{
			int spawn = player.GetComponent<PlayerScript>().spawn;
			switch(spawn){
				case 0:
					player.transform.position = sp;
					break;
				case 1:
					player.transform.position = cp1;
					break;
				case 2:
					player.transform.position = cp2;
					break;
				}
		}

		
	}

}
