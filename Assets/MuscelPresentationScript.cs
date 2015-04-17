using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class MuscelPresentationScript : MonoBehaviour
{
    private RunningListener listener;
    public Graphics g;
    public static Timer timer;
    


    public float turnSpeed = 50f;
    public float runSpeed = 4.5f;

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
    }

    void Update()
    {
        if (KinectGestures.running)
        {//listener.IsRunning())
            timer.Interval = 250;
            timer.Enabled = true;

        }
        if (KinectGestures.hurdling)
        {

        }
        if (timer.Enabled)// && !player.collider.)
        {
            //running
        }
    }



    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        //Debug.Log("timer event");

        timer.Enabled = false;
    }

}