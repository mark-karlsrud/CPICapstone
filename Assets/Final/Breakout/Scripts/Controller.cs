using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public static string GamePath = ".\\HighScore.ini";

	//////break out scores//////
	public static int[] highscore = new int[10];
    public static int theScore = 0;
	public static string name = "";
	public static string[] names = new string[10];
	//////break out scores//////

	//////muscle march scores//////
	public static int[] muscleTimes = new int[10];
	public static int myMuscleTime = 999999;
	public static string[] muscleNames = new string[10];
	public static string myMuscleName = "";
	public static int hits = 0;
	public static int timeAdded = 15;
	//////muscle march scores//////

	/// //////Clock scores//////
	public static int[] clockTimes = new int[10];
	public static int myClockTime = 999999;
	public static string[] clockNames = new string[10];
	public static string myClockName = "";
	//////Clock scores//////

	//////simon says scores//////
	public static int[] simonHighscore = new int[10];
	public static int simonScore = 0;
	public static string simonName = "";
	public static string[] simonNames = new string[10];
	//////simon says scores//////

    public static int muscleWallCount = 0;
    public static bool muscleAI1Dead;
    public static bool muscleAI2Dead;
    public static bool muscleAI3Dead;
    public static int muscleHealth = 4;

	void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		SerializeINI.DeSerialize ();
		Application.LoadLevel (0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(1);
		}
	}
}
