using UnityEngine;
using System.Collections;

public class simonsaysLeaderboard : MonoBehaviour {

	public GUISkin skin;
	public GUISkin large_skin;
	public GUISkin yourscore;


	//scalling matrix
	public float native_width = 800;
	public float native_height = 600;
	public float width_scale;
	public float height_scale;
	public static bool turnOff = false;
	
	void Awake()
	{
		width_scale = Screen.width/native_width;
		height_scale = Screen.height/native_height;
		//DontDestroyOnLoad(transform.gameObject);

	}

	void OnGUI()
	{
		GUI.matrix = Matrix4x4.TRS(new Vector3(0,0,0),
		                           Quaternion.identity,
		                           new Vector3(width_scale,height_scale,1));

		GUI.skin = large_skin;
		GUI.Label(new Rect(0, -20, 300, 80), "Game Over");

		GUI.skin = skin;
		GUI.BeginGroup(new Rect(native_width/2-240, 16, 300,500));
		GUI.Label(new Rect(60, 0, 300, 400), "Name:\n" + 
		          "\t1. "+Controller.simonNames[0]+"\n"+
		          "\t2. "+Controller.simonNames[1]+"\n"+
		          "\t3. "+Controller.simonNames[2]+"\n"+
		          "\t4. "+Controller.simonNames[3]+"\n"+
		          "\t5. "+Controller.simonNames[4]+"\n"+
		          "\t6. "+Controller.simonNames[5]+"\n"+
		          "\t7. "+Controller.simonNames[6]+"\n"+
		          "\t8. "+Controller.simonNames[7]+"\n"+
		          "\t9. "+Controller.simonNames[8]+"\n"+
		          "\t10. "+Controller.simonNames[9]);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(native_width/2-140, 16, 300,500));
		GUI.Label(new Rect(140, 0, 300, 400), "Highscore:\n" + 
		          "\t "+Controller.simonHighscore[0]+"\n"+
		          "\t "+Controller.simonHighscore[1]+"\n"+
		          "\t "+Controller.simonHighscore[2]+"\n"+
		          "\t "+Controller.simonHighscore[3]+"\n"+
		          "\t "+Controller.simonHighscore[4]+"\n"+
		          "\t "+Controller.simonHighscore[5]+"\n"+
		          "\t "+Controller.simonHighscore[6]+"\n"+
		          "\t "+Controller.simonHighscore[7]+"\n"+
		          "\t "+Controller.simonHighscore[8]+"\n"+
		          "\t "+Controller.simonHighscore[9]);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(native_width/2+100, native_height/2+150, 300,100));
		if(Controller.simonScore > Controller.simonHighscore[9])
			Controller.simonName = GUI.TextField(new Rect(50, 50, 200, 90), Controller.simonName,3);
		GUI.skin = yourscore;
		GUI.Label(new Rect(0, 0, 300, 50), "\t\tYour score: "+Controller.simonScore);
		GUI.EndGroup();

		GUI.skin = skin;
		if (GUI.Button (new Rect (native_width/2-130, native_height/2+200, 200, 60), "Restart"))
		{
			InteractionManager.usedOnce = false;
			GrabDropScript.grabbedPaddle = false;

			if(Controller.simonScore > Controller.simonHighscore[9])
			{
				Controller.simonHighscore[9] = Controller.simonScore;
				Controller.simonNames[9] = Controller.simonName;
			}
			
			for(int i = 8; i>-1; i--)
			{
				if(Controller.simonScore > Controller.simonHighscore[i])
				{
					Controller.simonHighscore[i+1] = Controller.simonHighscore[i];
					Controller.simonNames[i+1] = Controller.simonNames[i];
					Controller.simonHighscore[i] = Controller.simonScore;
					Controller.simonNames[i] = Controller.simonName;
				}
			}

			SerializeINI.Serialize();
			Controller.simonScore = 0;
			turnOff=true;
			Application.LoadLevel(0);
			}
	}

	// Use this for initialization
	void Start () {
		Controller.simonName = "AAA";
		Screen.showCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		Debug.Log (turnOff);
	}
}
