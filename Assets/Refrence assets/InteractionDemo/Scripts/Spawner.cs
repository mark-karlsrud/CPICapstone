using UnityEngine;
using System.Collections;
using System.Timers;


public class Spawner : MonoBehaviour {

	public GameObject ball, barrel;
	public float waitTimer =3.0f;
	public float waitTimer2 =5.0f;
	Timer timer,shootTimer;
	private bool goingUp;
	private bool shoot;
	private Random rand;
	
	void Start () 
	{
		rand = new Random ();

		//transform.Translate (Vector3.up * Time.deltaTime);
		timer = new Timer();
		timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		timer.Interval = 1500;
		timer.Enabled = true;
		shootTimer = new Timer();
		shootTimer.Elapsed += new ElapsedEventHandler(OnTimedEventShoot);
		shootTimer.Interval = 900;
		shootTimer.Enabled = true;
	
	}
	
	
	void Update () 
	{
		if (shoot)
		{
			Instantiate (ball,barrel.transform.position,Quaternion.identity);
			shoot = false;
		}
		if (goingUp) 
		{

			///Instantiate (ball);
			transform.Translate (Vector3.up * 0.1f);

		} else 
		{
			//Instantiate (ball,barrel.transform.position,Quaternion.identity);
			transform.Translate (Vector3.down * 0.1f);
		}

	}
		
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		goingUp = !goingUp;

		Debug.Log("timer event");
		//timer.Enabled = false;
	}
	private void OnTimedEventShoot(object source, ElapsedEventArgs e)
	{
		shoot = true;
		
		Debug.Log("timer event");
		//timer.Enabled = false;
	}
}

