using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PresentationScript : MonoBehaviour 
{
	private SimonSaysListener gestureListener;
	

	
	void Start() 
	{
		// hide mouse cursor
		Screen.showCursor = false;
				
		// get the gestures listener
		gestureListener = Camera.main.GetComponent<SimonSaysListener>();
	}
	
	void Update() 
	{
		// dont run Update() if there is no user
		KinectManager kinectManager = KinectManager.Instance;
		
	}
	
}
