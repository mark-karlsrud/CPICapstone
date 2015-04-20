using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;

public class SwitchScenes2 : MonoBehaviour {

	private bool goToLeaderBoard;

	private System.Timers.Timer finishedTimer;

	public void Start(){
		finishedTimer = new System.Timers.Timer ();
		finishedTimer.Elapsed += new ElapsedEventHandler(GoToLeaderBoard);
		finishedTimer.Interval = 2000; //2 second timer
		finishedTimer.Enabled = false;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag =="Player")
		{
			finishedTimer.Enabled = true;
			finishedTimer.Start();			
		}
	}

	public void Update(){
		if(goToLeaderBoard){
			goToLeaderBoard = false;
			Application.LoadLevel("ClockTLeaderB");
		}
	}

	//called when it's time to respawn
	private void GoToLeaderBoard(object source, ElapsedEventArgs e)
	{
		finishedTimer.Enabled = false;
		goToLeaderBoard = true;
	}
}
