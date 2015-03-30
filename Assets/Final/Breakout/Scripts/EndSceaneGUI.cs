using UnityEngine;
using System.Collections;

public class EndSceaneGUI : MonoBehaviour {

	public GUISkin skin;
	public GUISkin large_skin;
	public GUISkin yourscore;
	public Texture2D background;
	public Camera mainCamera;

	//scalling matrix
	public float native_width = 800;
	public float native_height = 600;
	public float width_scale;
	public float height_scale;
	public static bool turnOff = false;
	
	void Awake()
	{
		Destroy(mainCamera);
		width_scale = Screen.width/native_width;
		height_scale = Screen.height/native_height;
		//DontDestroyOnLoad(transform.gameObject);

	}

	void OnGUI()
	{
		if(Application.loadedLevel == 1)
		{
		GUI.matrix = Matrix4x4.TRS(new Vector3(0,0,0),
		                           Quaternion.identity,
		                           new Vector3(width_scale,height_scale,1));

		GUI.DrawTexture(new Rect(0,0,native_width,native_height),background);

		GUI.skin = large_skin;
		GUI.Label(new Rect(270, 40, 300, 80), "Game Over");

		GUI.skin = skin;
		GUI.BeginGroup(new Rect(native_width/2-300, 100, 300,500));
		GUI.Label(new Rect(0, 0, 300, 400), "Name:\n" + 
		          "\t\t\t1. "+Controller.names[0]+"\n"+
		          "\t\t\t2. "+Controller.names[1]+"\n"+
		          "\t\t\t3. "+Controller.names[2]+"\n"+
		          "\t\t\t4. "+Controller.names[3]+"\n"+
		          "\t\t\t5. "+Controller.names[4]+"\n"+
		          "\t\t\t6. "+Controller.names[5]+"\n"+
		          "\t\t\t7. "+Controller.names[6]+"\n"+
		          "\t\t\t8. "+Controller.names[7]+"\n"+
		          "\t\t\t9. "+Controller.names[8]+"\n"+
		          "\t\t\t10. "+Controller.names[9]);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(native_width/2-300, 100, 300,500));
		GUI.Label(new Rect(90, 0, 300, 400), "Highscore:\n" + 
		          "\t\t\t "+Controller.highscore[0]+"\n"+
		          "\t\t\t "+Controller.highscore[1]+"\n"+
		          "\t\t\t "+Controller.highscore[2]+"\n"+
		          "\t\t\t "+Controller.highscore[3]+"\n"+
		          "\t\t\t "+Controller.highscore[4]+"\n"+
		          "\t\t\t "+Controller.highscore[5]+"\n"+
		          "\t\t\t "+Controller.highscore[6]+"\n"+
		          "\t\t\t "+Controller.highscore[7]+"\n"+
		          "\t\t\t "+Controller.highscore[8]+"\n"+
		          "\t\t\t "+Controller.highscore[9]);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(native_width/2+50, native_height/2-100, 300,100));
		if(Controller.theScore > Controller.highscore[9])
			Controller.name = GUI.TextField(new Rect(50, 50, 200, 90), Controller.name,
			                              
			                                3);
		GUI.skin = yourscore;
		GUI.Label(new Rect(0, 0, 300, 50), "\t\tYour score: "+Controller.theScore);
		GUI.EndGroup();

		GUI.skin = skin;
		if (GUI.Button (new Rect (native_width/2-130, native_height/2+200, 200, 60), "Restart"))
		{	ScoreBoard.score=0;
			InteractionManager.usedOnce = false;
			GrabDropScript.grabbedPaddle = false;
			if(Controller.theScore > Controller.highscore[0])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.highscore[5];
				Controller.highscore[5] = Controller.highscore[4];
				Controller.highscore[4] = Controller.highscore[3];
				Controller.highscore[3] = Controller.highscore[2];
				Controller.highscore[2] = Controller.highscore[1];
				Controller.highscore[1] = Controller.highscore[0];
				Controller.highscore[0] = Controller.theScore;
				Controller.names[0] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[1])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.highscore[5];
				Controller.highscore[5] = Controller.highscore[4];
				Controller.highscore[4] = Controller.highscore[3];
				Controller.highscore[3] = Controller.highscore[2];
				Controller.highscore[2] = Controller.highscore[1];
				Controller.highscore[1] = Controller.theScore;
				Controller.names[1] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[2])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.highscore[5];
				Controller.highscore[5] = Controller.highscore[4];
				Controller.highscore[4] = Controller.highscore[3];
				Controller.highscore[3] = Controller.highscore[2];
				Controller.highscore[2] = Controller.theScore;
				Controller.names[2] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[3])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.highscore[5];
				Controller.highscore[5] = Controller.highscore[4];
				Controller.highscore[4] = Controller.highscore[3];
				Controller.highscore[3] = Controller.theScore;
				Controller.names[3] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[4])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.highscore[5];
				Controller.highscore[5] = Controller.highscore[4];
				Controller.highscore[4] = Controller.theScore;
				Controller.names[4] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[5])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.highscore[5];
				Controller.highscore[5] = Controller.theScore;
				Controller.names[5] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[6])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.highscore[6];
				Controller.highscore[6] = Controller.theScore;
				Controller.names[6] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[7])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.highscore[7];
				Controller.highscore[7] = Controller.theScore;
				Controller.names[7] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[8])
			{
				Controller.highscore[9] = Controller.highscore[8];
				Controller.highscore[8] = Controller.theScore;
				Controller.names[8] = Controller.name;
			}
			else if(Controller.theScore > Controller.highscore[9])
			{
				Controller.highscore[9] = Controller.theScore;
				Controller.names[9] = Controller.name;
			}

			SerializeINI.Serialize();
			Controller.theScore = 0;
			turnOff=true;
			Destroy(mainCamera);
			Application.LoadLevel(0);

		
			}
		}
	}

	// Use this for initialization
	void Start () {
		Controller.name = "AAA";
		Screen.showCursor = true;
	}
	
	// Update is called once per frame
	void Update () {
	
		Destroy(mainCamera);
		Debug.Log (turnOff);
	}
}
