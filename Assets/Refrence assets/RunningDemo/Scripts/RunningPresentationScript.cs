using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class RunningPresentationScript : MonoBehaviour 
{
	private GestureListener gestureListener;
    public GameObject player;
    System.Timers.Timer timer;
    //This is a test pt 2
	
	void Start() 
	{
		// hide mouse cursor
		Screen.showCursor = false;
			
		// get the gestures listener
		gestureListener = Camera.main.GetComponent<GestureListener>();
        timer = new System.Timers.Timer();
        timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        timer.Interval = 3000;
        timer.Enabled = false;
    }

    void Update()
    {
        if (!timer.Enabled && KinectGestures.running)
        {
            timer.Interval = 3000;
            timer.Enabled = true;
        }
        if (timer.Enabled)
        {
            player.transform.Translate(Vector3.forward * Time.deltaTime);
        }
        else
        {
            player.transform.Translate(Vector3.zero);
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Debug.Log("timer event");

        timer.Enabled = false;
    }
}
