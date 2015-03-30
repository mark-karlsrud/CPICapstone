using UnityEngine;
using System.Collections;
using System.Timers;



public class Spawner : MonoBehaviour {

	public GameObject ball, barrel,evilBall,healthBall,grabBall;
	public Camera camera;
	public GUIText Score;
	Timer timer,shootTimer, startTimer,halfWayTimer,endHalfTimer,gameOverTimer;
	private bool goingUp,gameover, changeShoot,rainHell;
	private bool shoot, shootHealthBall;
	public static bool startGame = false;
	public bool shootEvilBall;
	private Random rand;
	public float random,random2,random3 ;
	private GrabDropScript grabDrop;
	private int  nahbrah;

	void Start () 
	{
	
		rand = new Random ();
		startTimer = new Timer();
		startTimer.Elapsed += new ElapsedEventHandler(OnTimedEventStart);
		startTimer.Interval = 4000;
		startTimer.Enabled = false;
		timer = new Timer();
		timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		timer.Interval = 3000;
		timer.Enabled = true;
		shootTimer = new Timer();
		shootTimer.Elapsed += new ElapsedEventHandler(OnTimedEventShoot);
		shootTimer.Interval = 1400;
		shootTimer.Enabled = true;
		halfWayTimer = new Timer();
		halfWayTimer.Elapsed += new ElapsedEventHandler(OnTimedEventHalfWay);
		halfWayTimer.Interval = 45000;//5550;//45000;
		halfWayTimer.Enabled = true;
		endHalfTimer = new Timer();
		endHalfTimer.Elapsed += new ElapsedEventHandler(OnTimedEventEndHalf);
		endHalfTimer.Interval = 63000;//9000//63000;
		endHalfTimer.Enabled = true;

		grabDrop = camera.GetComponent<GrabDropScript> ();


	}

	void Update () 
	{
		Debug.Log (GrabDropScript.grabbedPaddle);
		//grabDrop
		nahbrah = ScoreBoard.score;
	
		if (!gameover) {
	
			if (GrabDropScript.grabbedPaddle)
			{
				startTimer.Enabled=true;
			}
			if(startGame==true)
			{
				if(shootTimer.Interval==900)
				{
					random = Random.Range (8000f, 10900.5f);
					random2 = Random.Range (5000f, 10900.5f);
					random3 = Random.Range (1000f, 10900.5f);
				}

				else
				{
					random = Random.Range (0f, 10890.5f);
					random2 = Random.Range (0f, 10890.5f);
					random3 = Random.Range (0f, 10890.5f);
				}


				if (shoot) {
					GameObject newball = (GameObject)Instantiate (ball, barrel.transform.position, Quaternion.identity);
					shoot = false;
					//grabDrop.draggableObjects.Add (newball);
				}
				if (goingUp) {

					transform.Translate (Vector3.up * 0.1f);

				} else {

					transform.Translate (Vector3.down * 0.1f);
				}
				if (random > 10880) 
				{

					Instantiate (evilBall, barrel.transform.position, Quaternion.identity);
					grabDrop.draggableObjects.Add (evilBall);
					//shootEvilBall = false;

				}

				if (random2 > 10887) {//&& shootEvilBall ) 
					Instantiate (healthBall, barrel.transform.position, Quaternion.identity);
					grabDrop.draggableObjects.Add (healthBall);
				
				}
				
				if (random3 > 10880){//10800){//10100) {//&& shootEvilBall ) 
					GameObject newGrabBall = (GameObject)Instantiate (grabBall, barrel.transform.position, Quaternion.identity);
					grabDrop.draggableObjects.Add (newGrabBall);
				}
				//change direction of ball shooting
				if(changeShoot)
				{
					ball.rigidbody.constraints = RigidbodyConstraints.None;

					if(shootTimer.Interval==700)
					{
						changeShoot=false;
						//Debug.Log(changeShoot);

					}
				}
				else
				{
					ball.rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
				}
			}


		} else {
		}
		Debug.Log (ScoreBoard.score);
		Score.text = ("Score " + nahbrah);
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

	private void OnTimedEventHalfWay(object source, ElapsedEventArgs e)
	{
		shootTimer.Interval = 900;
		halfWayTimer.Interval = 40000;
		changeShoot = true;
		//halfWayTimer.Enabled = false;
	}

	private void OnTimedEventEndHalf(object source, ElapsedEventArgs e)
	{
		shootTimer.Interval = 700;
		halfWayTimer.Enabled = false;
	}





}


