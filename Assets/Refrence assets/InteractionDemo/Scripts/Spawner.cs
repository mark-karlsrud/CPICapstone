using UnityEngine;
using System.Collections;
using System.Timers;


public class Spawner : MonoBehaviour {

	public GameObject ball, barrel;
	public float waitTimer =3.0f;
	public float waitTimer2 =5.0f;
	Timer timer;
	private bool goingUp;
	
	
	void Start () 
	{
		//Instantiate (ball);
		//transform.Translate (Vector3.up * Time.deltaTime);
		timer = new Timer();
		timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		timer.Interval = 3000;
		timer.Enabled = true;
	}
	
	
	void Update () 
	{
		if (goingUp) 
		{
			transform.Translate (Vector3.up * 0.1f);

		} else 
		{

			transform.Translate (Vector3.down * 0.1f);
		}

	}
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		goingUp = !goingUp;
	
		Debug.Log("timer event");
		//timer.Enabled = false;
	}
}

