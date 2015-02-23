using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;


public class SimonSays : MonoBehaviour
{
	public int currentIndex = 0;
	public GUIText Input;
	public GUIText Output;
	public GUIText Prompt;
    public GUIText Score;
    private bool pickPose;
    public GUIText timeLimitText;

    public int timeLimit = 10;
    
	private Timer timer;
    private Timer poseTimer;

    private int timeRemaining;

	string[] Poses = new string[2]{
		  "Tpose",
	    //  "RightBicepFlex",
		//  "LeftBicepFlex",
		  "SwipeUp",
		//  "SwipeDown",
		//  "SwipeLeft",
		//  "SwipeRight",
		//	"Psi"
    };

	// Use this for initialization
	void Start () {
        timer = new Timer();
        timer.Enabled = false;
        poseTimer = new Timer();
        poseTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
        poseTimer.Interval = 1000; //decrement every 1 seconds
        poseTimer.Enabled = true;

        pickPose = true;
        Score.guiText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        if (pickPose)
            pickAPose();
		if(!timer.Enabled)
			checkPose ();
        timeLimitText.guiText.text = "TIME TO COMPLETE POSE: " + timeRemaining.ToString();
	}

	public void pickAPose ()
	{
        Input.guiText.text = "";
		System.Random rand = new System.Random();
		currentIndex = rand.Next(0,Poses.Length);
		Prompt.guiText.text = "Do the following pose: " + Poses[currentIndex];
        pickPose = false;
        timeRemaining = timeLimit;
	}

	public void checkPose(){
		string doingPose = Input.guiText.text.Replace(" detected","");
		if(doingPose == Poses[currentIndex]){
			Output.guiText.text = "SUCCESS";
            Prompt.guiText.text = "";
            int score = Convert.ToInt32(Score.guiText.text);
            Score.guiText.text = (score + 1).ToString();
            
            if (!timer.Enabled)
            {
                timer = new Timer();
                timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                timer.Interval = 3000; //wait 3 seconds
                timer.Enabled = true;
            }
		}
		else{
			Output.guiText.text = "FAIL";
		}
	}

	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
        timer.Enabled = false;
        pickPose = true;
	}

    private void OnTimedEvent2(object source, ElapsedEventArgs e)
    {
        timeRemaining--;
        if (timeRemaining == 0)
        {
            poseTimer.Enabled = false;
        }
    }
}
