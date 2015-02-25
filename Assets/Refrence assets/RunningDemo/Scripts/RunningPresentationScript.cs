using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Timers;

public class RunningPresentationScript : MonoBehaviour 
{
	private RunningListener listener;
    public CharacterController player;
    Timer timer;
    
    public float turnSpeed = 50f;
	
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

        /*
        float jumpSpeed = 100.0F;
        float gravity = 20.0F;
        Vector3 moveDirection = Vector3.zero;
        moveDirection.y = jumpSpeed;
        moveDirection.y -= gravity * Time.deltaTime;
        player.Move(moveDirection * Time.deltaTime);
        */
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
            timer.Interval = 2000;
            timer.Enabled = true;
        }
        if (timer.Enabled)
        {
            player.transform.Translate(6 * Time.deltaTime * player.transform.forward);
        }
        else
        {
            player.transform.Translate(Vector3.zero);
        }


        if (listener.IsJumping())
        {
            /*if (timer.Enabled)
            {
                player.transform.Translate(2 * Time.deltaTime * (player.transform.forward + player.transform.up));
            }
            else
                player.rigidbody.AddForce(player.transform.up * 250);
             * */

            //float speed = 6.0F;
            float jumpSpeed = 200.0F;
            float gravity = 20.0F;
            Vector3 moveDirection = Vector3.zero;
            moveDirection.y = jumpSpeed;
            moveDirection.y -= gravity * Time.deltaTime;
            player.Move(moveDirection * Time.deltaTime);
        }

        //TURN LEFT
        if (KinectGestures.turnLeft)//listener.IsLeftTurn())
        {
            player.transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
        //TURN RIGHT
        else if (KinectGestures.turnRight)//listener.IsRightTurn())
        {
            player.transform.Rotate(Vector3.down, -turnSpeed * Time.deltaTime);
        }
        else
        {
            player.transform.Rotate(Vector3.down, 0f);
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        Debug.Log("timer event");

        timer.Enabled = false;
    }
}
