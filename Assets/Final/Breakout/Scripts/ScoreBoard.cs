using UnityEngine;
using System.Collections;
using System.Timers;

public class ScoreBoard : MonoBehaviour {

	public static int score;
	public GameObject player,spawner;
	public bool destroy =false; 
	public Camera mainCamera;

	Timer gameOverTimer;
	// Use this for initialization
	void Start () 
	{
		gameOverTimer = new Timer();
		gameOverTimer.Elapsed += new ElapsedEventHandler(OnTimedEventEndTime);
		gameOverTimer.Interval =80000; //100000;//10000;
		gameOverTimer.Enabled = true;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
			if (destroy) 
			{
			Destroy (spawner);
			ChangeScene s = gameObject.AddComponent<ChangeScene>();
			s.scene = "leaderboard";
			s.waitTime=10;//10;
			destroy=false;
			}

	}
	private void OnTimedEventEndTime(object source, ElapsedEventArgs e)
	{

		destroy = true;
		gameOverTimer.Enabled = false;

	}
}
