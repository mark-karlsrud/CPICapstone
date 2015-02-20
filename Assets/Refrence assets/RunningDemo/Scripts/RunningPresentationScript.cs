using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class RunningPresentationScript : MonoBehaviour 
{
	private RunningListener listener;
    public GameObject player;
    System.Timers.Timer timer;
    //This is a test pt 2
	
	void Start() 
	{
		// hide mouse cursor
		Screen.showCursor = false;
			
		// get the gestures listener
		listener = Camera.main.GetComponent<RunningListener>();
        timer = new System.Timers.Timer();
        timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
        timer.Interval = 3000;
        timer.Enabled = false;

        //player.rigidbody.AddForce(player.transform.up * 250);
        //player.transform.Translate(1f * Vector3.forward);
    }

    void Update()
    {
      
        //player.transform.Translate(0.01f * Vector3.forward);
     

        if (KinectGestures.running)//listener.IsRunning())//
        {
            Debug.Log("RUNNING");
        }
        if (!timer.Enabled && KinectGestures.running)//listener.IsRunning())
        {
            timer.Interval = 1000;
            timer.Enabled = true;
        }
        if (timer.Enabled)
        {
            player.transform.Translate(2 * Time.deltaTime * player.transform.forward);
        }
        else
        {
            player.transform.Translate(Vector3.zero);
        }
        if (listener.IsJumping())
        {
            if (timer.Enabled)
            {
                player.transform.Translate(2 * Time.deltaTime * (player.transform.forward + player.transform.up));
            }
            else
                player.rigidbody.AddForce(player.transform.up * 250);
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Debug.Log("timer event");

        timer.Enabled = false;
    }
}
