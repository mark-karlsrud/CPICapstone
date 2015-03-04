using UnityEngine;
using System.Collections;
using System.Timers;


public class Spawner : MonoBehaviour {

	public GameObject ball, barrel,evilBall,healthBall;
	Timer timer,shootTimer, startTimer;
	private bool goingUp;
	private bool shoot, shootHealthBall,startGame;
	public bool shootEvilBall;
	private Random rand;
	public float random,random2 ;
	
	void Start () 
	{
	
		rand = new Random ();

		startTimer = new Timer();
		startTimer.Elapsed += new ElapsedEventHandler(OnTimedEventStart);
		startTimer.Interval = 1000;
		startTimer.Enabled = true;
		timer = new Timer();
		timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		timer.Interval = 3000;
		timer.Enabled = true;
		shootTimer = new Timer();
		shootTimer.Elapsed += new ElapsedEventHandler(OnTimedEventShoot);
		shootTimer.Interval = 2500;
		shootTimer.Enabled = true;

	
	}

	void Update () 
	{
		if (startGame) {
			random = Random.Range (0f, 10900.5f);
			random2 = Random.Range (0f, 10900.5f);
			if (shoot) {
				Instantiate (ball, barrel.transform.position, Quaternion.identity);
				shoot = false;
			}
			if (goingUp) {

				transform.Translate (Vector3.up * 0.1f);

			} else {

				transform.Translate (Vector3.down * 0.1f);
			}
			if (random > 10880) {
				//shootEvilBall = true;
				Instantiate (evilBall, barrel.transform.position, Quaternion.identity);
				//shootEvilBall = false;

			}
			if (random2 > 10888) {//&& shootEvilBall ) 
				Instantiate (healthBall, barrel.transform.position, Quaternion.identity);
			}

		}
	}
		
	private void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		goingUp = !goingUp;
		

	}
	private void OnTimedEventShoot(object source, ElapsedEventArgs e)
	{
		shoot = true;
		

	
	}
	private void OnTimedEventStart(object source, ElapsedEventArgs e)
	{

		startGame = true;
	}
}


