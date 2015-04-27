using UnityEngine;
using System.Collections;

public class muscleManScoreboardGUI : MonoBehaviour {

	public GUISkin skin;
	public GUISkin large_skin;
	public GUISkin yourscore;

	//scalling matrix
	public float native_width = 800;
	public float native_height = 600;
	public float width_scale;
	public float height_scale;
	public static bool turnOff = false;
	private string[] seconds = new string[10];
	
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
		GUI.Label(new Rect(270, 40, 300, 80), "Game Over");

		GUI.skin = skin;
		GUI.BeginGroup(new Rect(native_width/2-200, 100, 300,500));
		GUI.Label(new Rect(30, 0, 300, 400), "Name:\n" + 
		          "\t1. "+Controller.muscleNames[0]+"\n"+
		          "\t2. "+Controller.muscleNames[1]+"\n"+
		          "\t3. "+Controller.muscleNames[2]+"\n"+
		          "\t4. "+Controller.muscleNames[3]+"\n"+
		          "\t5. "+Controller.muscleNames[4]+"\n"+
		          "\t6. "+Controller.muscleNames[5]+"\n"+
		          "\t7. "+Controller.muscleNames[6]+"\n"+
		          "\t8. "+Controller.muscleNames[7]+"\n"+
		          "\t9. "+Controller.muscleNames[8]+"\n"+
		          "\t10. "+Controller.muscleNames[9]);
		GUI.EndGroup();

		for(int i = 0; i<10; i++)
		{
			if(Controller.muscleTimes[i]%60f < 10)
				seconds[i] = "0"+Controller.muscleTimes[0]%60f;
			else
				seconds[i] = ""+Controller.muscleTimes[0]%60f;
		}

		GUI.BeginGroup(new Rect(native_width/2-200, 100, 300,500));
		GUI.Label(new Rect(170, 0, 300, 400), "Time:\n" + 
		          "\t "+Mathf.Floor(Controller.muscleTimes[0]/60f)+":"+seconds[0]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[1]/60f)+":"+seconds[1]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[2]/60f)+":"+seconds[2]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[3]/60f)+":"+seconds[3]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[4]/60f)+":"+seconds[4]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[5]/60f)+":"+seconds[5]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[6]/60f)+":"+seconds[6]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[7]/60f)+":"+seconds[7]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[8]/60f)+":"+seconds[8]+"\n"+
		          "\t "+Mathf.Floor(Controller.muscleTimes[9]/60f)+":"+seconds[9]);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(native_width/2+70, native_height/2-100, 300,100));
		if((Controller.myMuscleTime + Controller.hits*Controller.timeAdded) < Controller.muscleTimes[9])
			Controller.myMuscleName = GUI.TextField(new Rect(50, 50, 200, 90), Controller.myMuscleName,3);
		GUI.skin = yourscore;
		GUI.Label(new Rect(0, 0, 300, 50), "\t\tYour score: "+Controller.theScore);
		GUI.EndGroup();

		GUI.skin = skin;
		if (GUI.Button (new Rect (native_width/2-130, native_height/2+200, 200, 60), "Restart"))
		{	
			InteractionManager.usedOnce = false;
			GrabDropScript.grabbedPaddle = false;

			if((Controller.myMuscleTime + Controller.hits*Controller.timeAdded) < Controller.muscleTimes[9])
			{
				Controller.muscleTimes[9] = (Controller.myMuscleTime + Controller.hits*Controller.timeAdded);
				Controller.muscleNames[9] = Controller.myMuscleName;
			}

			for(int i = 8; i>-1; i--)
			{
				if((Controller.myMuscleTime + Controller.hits*Controller.timeAdded) < Controller.muscleTimes[i])
				{
					Controller.muscleTimes[i+1] = Controller.muscleTimes[i];
					Controller.muscleNames[i+1] = Controller.muscleNames[i];
					Controller.muscleTimes[i] = (Controller.myMuscleTime + Controller.hits*Controller.timeAdded);
					Controller.muscleNames[i] = Controller.myMuscleName;
				}
			}
			SerializeINI.Serialize();
			turnOff=true;
			Controller.myMuscleTime=0;
			Controller.hits = 0;
			Application.LoadLevel(0);

		
			}
	}

	// Use this for initialization
	void Start () {
		Controller.myMuscleName = "AAA";
		Screen.showCursor = true;
	}

}