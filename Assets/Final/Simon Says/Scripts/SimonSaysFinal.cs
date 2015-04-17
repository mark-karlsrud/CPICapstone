using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;


public class SimonSaysFinal : MonoBehaviour
{
	public GUIText InputText;
	public GUIText Output;
	public GUIText Prompt;
	public GUIText Score;
	public GUIText timeLimitText;
	public GUITexture checkMark,redX, nextRoundTexture;
	private bool pickPose,simonsTurn,listenToUser; //stages of game
	private bool fail;
	public bool madeIt = false;
	
	public string[] poseNames;
	public int numberOfPosesToStart;
	public int score;
	
	bool start = true;
	bool end;
	bool showCheckmark,showX;
	
	int previousIndex;
	
	public Queue <int> posesList = new Queue<int> ();
	public Queue<int> posesToPlay = new Queue<int>();
	private System.Timers.Timer  poseTimer,endRoundTimer,checkmarkTimer,xTimer;
	
	private int timeRemaining,strikes;
	
	private System.Random rand;
	
	string[] Poses = new string[7]{
		
		"LeftBicepFlex",
        "RightBicepFlex",
        "RaiseLeftHand",
		"RaiseRightHand",
		"Tpose",
		"SwipeLeft",
		"SwipeRight"		
	};
	
	private int currentPose;
	
	// Use this for initialization
	void Start () 
	{
        nextRoundTexture.enabled = false;

		endRoundTimer = new System.Timers.Timer ();
		endRoundTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		endRoundTimer.Interval = 5000; //5 seconds between rounds
		endRoundTimer.Enabled = false;

		
		poseTimer = new System.Timers.Timer ();
		poseTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
		poseTimer.Interval = 1000; //decrement every 1 second
		poseTimer.Enabled = false;

		checkmarkTimer = new System.Timers.Timer ();
		checkmarkTimer.Elapsed += new ElapsedEventHandler(RemoveCheckmark);
		checkmarkTimer.Interval = 1000; //decrement every 1 second
		checkmarkTimer.Enabled = false;

		xTimer = new System.Timers.Timer ();
		xTimer.Elapsed += new ElapsedEventHandler(RemoveX);
		xTimer.Interval = 1000; //decrement every 1 second
		xTimer.Enabled = false;
		
		rand = new System.Random();
		
		Score.guiText.text = "0";
		timeLimitText.guiText.text = "-";
		
		//Initialize pose list with poses
		
		numberOfPosesToStart = 1;
		for (int i = 0; i < numberOfPosesToStart; i++)
		{
			pickAPose();
		}
		posesToPlay = new Queue<int>(posesList);
		
		simonsTurn = true;
	}
	
	// Update is called once per frame
	void Update () {

		Score.guiText.text = (score).ToString();
		
		/*
         * Stages of game
         */
		
		if (pickPose) //Time to add another pose to the list (new round)
		{
            nextRoundTexture.enabled = false;
			if(!fail) //do not add pose if previously failed
				pickAPose();
			pickPose = false;
			simonsTurn = true;
			posesToPlay = new Queue<int>(posesList);
		}
		else if (simonsTurn)
		{
			poseTimer.Enabled = false;
			animateSimon();
		}
		else if (listenToUser)
		{
			//Debug.Log("listen to user");
			listenToPose();
		}
		/* end of stages */

		if (showCheckmark) {
			checkMark.enabled = true;
			redX.enabled = false;
		} else {
			checkMark.enabled = false;
		}
		if (showX) {
			redX.enabled = true;
			checkMark.enabled = false;
		} else {
			redX.enabled = false;
		}
		
		
		if (poseTimer.Enabled)
		{
			timeLimitText.guiText.text = timeRemaining.ToString();
		}
		else
		{
			timeLimitText.guiText.text = "-";
		}

		if (endRoundTimer.Enabled) {
            if (fail)
                Output.guiText.text = "You failed. Strikes:" + strikes.ToString();
            else
            {
                Output.guiText.text = "You did it!";
                nextRoundTexture.enabled = true;
            }
		}
		
	}
	
	public void pickAPose ()
	{
		int newPose = rand.Next(0,Poses.Length);
		posesList.Enqueue(newPose);        
	}
	
	public void listenToPose()
	{
		//temporary
		Output.guiText.text = "do this pose: " + Poses[posesToPlay.Peek()];
		
		
		string doingPose = InputText.guiText.text.Replace(" detected", "");
        InputText.guiText.text = "";
        if(doingPose.Contains("Right"))
			doingPose = doingPose.Replace("Right","Left");
		else {
			doingPose = doingPose.Replace("Left","Right");
		}
		
		if (doingPose != "")
		{
			
			//Debug.Log("doingPose:" + doingPose);
			
			//Get Pose idex
			int poseIndex = -1;
			for (int i = 0; i < Poses.Length; i++)
			{
				if (Poses[i] == doingPose)
				{
					poseIndex = i;
					break;
				}
			}
			
			//Debug.Log("poseIndex:" + poseIndex);
			
			//Correct pose
			if (poseIndex == posesToPlay.Peek() && poseIndex != -1)
			{
                Debug.Log("You successfully did " + doingPose + ", which is " + Poses[posesToPlay.Peek()]);
				Output.guiText.text = "SUCCESS";

				posesToPlay.Dequeue();
				InputText.guiText.text = "";
				
				Prompt.guiText.text = "";
				
				/*
                if (!poseTimer.Enabled)
                {
                    poseTimer = new Timer();
                    poseTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                    poseTimer.Interval = 1000;
                    poseTimer.Enabled = true;
                }
                */

				showCheckmark = true;
				checkmarkTimer.Enabled = true;
				checkmarkTimer.Start ();

				//Debug.Log("size:"+posesToPlay.Count);
				//user has completed all poses
				if(posesToPlay.Count == 0){
					score++;
					strikes = 0;
					listenToUser = false;
					fail = false;
					endRoundTimer.Enabled = true;
					poseTimer.Enabled = false;
					endRoundTimer.Start ();
				}
			}
			else if(poseIndex != -1) //Not the right pose
			{
                Debug.Log("You did " + doingPose + " instead of " + Poses[posesToPlay.Peek()]);
				InputText.guiText.text = "";
				showX = true;
				xTimer.Enabled = true;
				xTimer.Start ();
			}
		}
		else
		{
			//Debug.Log("nothing");
		}
	}
	
	public void animateSimon()
	{
		if (posesToPlay.Count == 0 && !animation.isPlaying)
		{
			posesToPlay = new Queue<int>(posesList);
			simonsTurn = false;
            InputText.guiText.text = "";
			listenToUser = true;
			fail = false;
			timeRemaining = 5 * posesList.Count; // 5 seconds for every pose
			
			poseTimer.Enabled = true;
			poseTimer.Start ();
		}
		else if (!animation.isPlaying)//IsPlaying(posesToPlay.Peek()))
		{
			if(start){
				int poseToPlay = posesToPlay.Peek();
				this.animation.Play(poseNames[poseToPlay]);
				
				if(poseNames[poseToPlay].Contains("start")){
					end = true;
					start = false;
				}
				else{
					posesToPlay.Dequeue();
				}
			}
			else if(end){
				int poseToPlay = posesToPlay.Dequeue();
				this.animation.Play(poseNames[poseToPlay].Replace("start","end"));
				start = true;
				end = false;
			}
		}
		//else, wait until animation has finished
	}

	//Called at end of wait at end of round
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		endRoundTimer.Enabled = false;
		pickPose = true;
	}

	//Called every second when listening to user
	private void OnTimedEvent2(object source, ElapsedEventArgs e)
	{
		timeRemaining--;

		if (timeRemaining == 0) //FAILED TO COMPLETE POSE
		{
			strikes ++;
			poseTimer.Enabled = false;
			fail = true;
			listenToUser = false;
			endRoundTimer.Enabled = true;
			endRoundTimer.Start ();
		}
	}

	//called after a second to remove checkmark
	private void RemoveCheckmark(object source, ElapsedEventArgs e)
	{
		checkmarkTimer.Enabled = false;
		showCheckmark = false;
	}

	//called after a second to remove red x
	private void RemoveX(object source, ElapsedEventArgs e)
	{
		xTimer.Enabled = false;
		showX = false;
	}
}
