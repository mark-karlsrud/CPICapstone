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
		sp = new Vector3(SpawnPoint.position.x,SpawnPoint.position.y + 2,SpawnPoint.position.z);
		cp1 = new Vector3(CheckPoint1.position.x,CheckPoint1.position.y + 2,CheckPoint1.position.z);
		cp2 = new Vector3(CheckPoint2.position.x,CheckPoint2.position.y + 2,CheckPoint2.position.z);

		player.transform.position = sp;
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
