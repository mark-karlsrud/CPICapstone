using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public static string GamePath = ".\\HighScore.ini";
	public static int[] highscore = new int[10];
	public static int theScore = 0;
	public static string name = "";
	public static string[] names = new string[10];

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
		theScore = ScoreBoard.score;
	}
}
