using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;

public class Respawn : MonoBehaviour {
	
	public GameObject player;

	public Transform SpawnPoint;
	public Transform CheckPoint1;
	public Transform CheckPoint2;
	
	public Vector3 sp, cp1, cp2;
	
	private System.Timers.Timer respawnTimer;

	private PlayerScript playerScript;
	private CharacterController characterController;
	private MouseLook mouseLook;
	private bool timeToRespawn;

	// Use this for initialization
	void Start () {
		respawnTimer = new System.Timers.Timer ();
		respawnTimer.Elapsed += new ElapsedEventHandler(RespawnMe);
		respawnTimer.Interval = 2000; //2 second timer
		respawnTimer.Enabled = false;

		sp = new Vector3(SpawnPoint.position.x,SpawnPoint.position.y + 2,SpawnPoint.position.z);
		cp1 = new Vector3(CheckPoint1.position.x,CheckPoint1.position.y + 2,CheckPoint1.position.z);
		cp2 = new Vector3(CheckPoint2.position.x,CheckPoint2.position.y + 2,CheckPoint2.position.z);

		player.transform.position = sp;

		playerScript = player.GetComponent<PlayerScript>();
		characterController = player.GetComponent<CharacterController>();
		mouseLook = player.GetComponent<MouseLook>();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToRespawn){
			timeToRespawn = false;
			int spawn = playerScript.spawn;
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
			characterController.enabled = true;
			mouseLook.enabled = true;
		}
	}

	
	void OnTriggerEnter(Collider col)
	{

		if(col.tag == "Player")
		{
			characterController.enabled = false;
			mouseLook.enabled = false;
			respawnTimer.Enabled = true;
			respawnTimer.Start();
		}

		
	}

	//called when it's time to respawn
	private void RespawnMe(object source, ElapsedEventArgs e)
	{
		respawnTimer.Enabled = false;
		timeToRespawn = true;
	}

}
