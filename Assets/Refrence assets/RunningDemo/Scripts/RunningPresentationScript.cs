using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class RunningPresentationScript : MonoBehaviour 
{
	private GestureListener gestureListener;
    System.Timers.Timer timer;
    //This is a test
	
	void Start() 
	{
		// hide mouse cursor
		Screen.showCursor = false;
			
		// get the gestures listener
		gestureListener = Camera.main.GetComponent<GestureListener>();
        timer = new System.Timers.Timer();
        timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        timer.Interval = 3000;
        timer.Enabled = true;
    }

    void Update()
    {
        if (!timer.Enabled)
        {
            timer.Interval = 3000;
            timer.Enabled = true;
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Debug.Log("timer event");
        timer.Enabled = false;
    }
}
