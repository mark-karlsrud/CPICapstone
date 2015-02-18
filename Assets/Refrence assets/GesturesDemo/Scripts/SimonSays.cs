using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;


public class SimonSays : MonoBehaviour 
{
	public int currentIndex = 0;
	private bool freeze = false;
	public GUIText Input;
	public GUIText Output;
	public GUIText Prompt;
	public bool timerEnabled= false;



	private Timer timer;

	string[] Poses = new string[8]{
		"Tpose",
	    "RightBicepFlex",
		  "LeftBicepFlex",
		  "SwipeUp",
		  "SwipeDown",
		  "SwipeLeft",
		  "SwipeRight",
			"Psi"};

	// Use this for initialization
	void Start () {
		pickAPose();
	}
	
	// Update is called once per frame
	void Update () {
		if(!freeze)
			checkPose ();
	}

	public void pickAPose ()
	{
		System.Random rand = new System.Random();
		currentIndex = rand.Next(0,Poses.Length);
		Prompt.guiText.text = "Do the following pose: " + Poses[currentIndex];
	}

	public void checkPose(){
		string doingPose = Input.guiText.text.Replace(" detected","");
		if(doingPose == Poses[currentIndex]){
			Output.guiText.text = "SUCCESS";
			pickAPose ();
			/*
			freeze = true;
			timerEnabled = true;
			timer = new Timer();
			timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			timer.Interval = 3000;
			timer.Enabled = true;
			*/
		}
		else{
			Output.guiText.text = "FAIL";
		}
	}

	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		pickAPose ();
		timer.Enabled = false;
		freeze = false;
		timerEnabled = false;
		
	}
}
