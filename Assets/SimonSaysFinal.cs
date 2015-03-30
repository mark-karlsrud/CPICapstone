using System.Threading;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using System;


public class SimonSaysFinal : MonoBehaviour
{
	public GUIText InpuT;
	public GUIText Output;
	public GUIText Prompt;
	public GUIText Score;
	public GUIText timeLimitText;
	private bool pickPose,simonsTurn,listenToUser; //stages of game
	private bool fail;
	public bool madeIt = false;
	
	public string[] poseNames;
	public int numberOfPosesToStart;
	public int score;
	
	bool start = true;
	bool end;
	
	int previousIndex;
	
	public Queue <int> posesList = new Queue<int> ();
	public Queue<int> posesToPlay = new Queue<int>();
	private System.Timers.Timer  poseTimer,waitingTimer;
	
	private int timeRemaining,previousPoseCount;
	
	private System.Random rand;
	
	string[] Poses = new string[5]{
		
		"LeftBicepFlex",
		"RaiseRightHand",
		"Tpose",
		"SwipeLeft",
		"SwipeRight"
		//"SwipeDownLeft",
		//"SwipeDownRight"
		
	};
	
	private int currentPose;
	
	// Use this for initialization
	void Start () 
	{
		/*
	}
	void s(){
	*/

		/*
		waitingTimer = new System.Timers.Timer ();
		waitingTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		waitingTimer.Interval = 5000 * posesList.Count; //5 seconds for every pose
		waitingTimer.Enabled = true;
		waitingTimer.Start ();
		*/
		
		poseTimer = new System.Timers.Timer ();
		poseTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
		poseTimer.Interval = 1000; //decrement every 1 second
		poseTimer.Enabled = false;
		
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
		/*
		if (!this.animation.isPlaying && start) {
						this.animation.Play ("raise left hand start");
			start = false;
			end = true;
				}
		if (!this.animation.isPlaying && end) {
						this.animation.Play ("raise left hand end");
			end = false;
			start = true;
				}
*/
		Score.guiText.text = (score).ToString();
		
		/*
         * Stages of game
         */
		
		if (pickPose) //Time to add another pose to the list (new round)
		{
			pickAPose();
			pickPose = false;
			simonsTurn = true;
			posesToPlay = new Queue<int>(posesList);
		}
		else if (simonsTurn)
		{
			animateSimon();
			//timeRemaining = timeLimit;
			poseTimer.Enabled = true;
		}
		else if (listenToUser)
		{
			//Debug.Log("listen to user");
			listenToPose();
		}
		/* end of stages */
		

		if (!poseTimer.Enabled)
		{
			timeLimitText.guiText.text = timeRemaining.ToString();
		}
		else
		{
			timeLimitText.guiText.text = "-";
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
		
		
		string doingPose = InpuT.guiText.text.Replace(" detected", "");
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
			if (poseIndex == posesToPlay.Peek())
			{
				Output.guiText.text = "SUCCESS";
				score++;
				posesToPlay.Dequeue();
				InpuT.guiText.text = "";
				
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
				Debug.Log("size:"+posesToPlay.Count);
				//user has completed all poses
				if(posesToPlay.Count == 0){
					posesToPlay = new Queue<int>(posesList);
					listenToUser = false;
					pickPose = true;
				}
			}
			else //Not the right pose
			{
				//Output.guiText.text = "Not the right pose";
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
			listenToUser = true;
			timeRemaining = 5 * posesList.Count; // 5 seconds for every pose
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
	
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		waitingTimer.Enabled = false;
		pickPose = true;
	}
	
	private void OnTimedEvent2(object source, ElapsedEventArgs e)
	{
		//if (!waitingTimer.Enabled)
		//{
		timeRemaining--;
		//}
		if (timeRemaining == 0) //FAILED TO COMPLETE POSE
		{
			score--;
			poseTimer.Enabled = false;
			fail = true;
			Output.guiText.text = "TIME IS UP";
		}
	}
}
