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

		GUI.BeginGroup(new Rect(native_width/2-200, 100, 300,500));
		GUI.Label(new Rect(170, 0, 300, 400), "Time:\n" + 
		          "\t "+Controller.muscleScores[0]+"\n"+
		          "\t "+Controller.muscleScores[1]+"\n"+
		          "\t "+Controller.muscleScores[2]+"\n"+
		          "\t "+Controller.muscleScores[3]+"\n"+
		          "\t "+Controller.muscleScores[4]+"\n"+
		          "\t "+Controller.muscleScores[5]+"\n"+
		          "\t "+Controller.muscleScores[6]+"\n"+
		          "\t "+Controller.muscleScores[7]+"\n"+
		          "\t "+Controller.muscleScores[8]+"\n"+
		          "\t "+Controller.muscleScores[9]);
		GUI.EndGroup();

		GUI.BeginGroup(new Rect(native_width/2+70, native_height/2-100, 300,100));
		if(Controller.myMuscleScore > Controller.muscleScores[9])
			Controller.myMuscleName = GUI.TextField(new Rect(50, 50, 200, 90), Controller.myMuscleName,3);
		GUI.skin = yourscore;
        GUI.Label(new Rect(0, 0, 300, 50), "\t\tYour score: " + Controller.myMuscleScore);
		GUI.EndGroup();

		GUI.skin = skin;
		if (GUI.Button (new Rect (native_width/2-130, native_height/2+200, 200, 60), "Restart"))
		{	
			InteractionManager.usedOnce = false;
			GrabDropScript.grabbedPaddle = false;

			if(Controller.myMuscleScore > Controller.muscleScores[9])
			{
				Controller.muscleScores[9] = Controller.myMuscleScore;
				Controller.muscleNames[9] = Controller.myMuscleName;
			}

			for(int i = 8; i>-1; i--)
			{
				if(Controller.myMuscleScore > Controller.muscleScores[i])
				{
					Controller.muscleScores[i+1] = Controller.muscleScores[i];
					Controller.muscleNames[i+1] = Controller.muscleNames[i];
					Controller.muscleScores[i] = Controller.myMuscleScore;
					Controller.muscleNames[i] = Controller.myMuscleName;
				}
			}
			SerializeINI.Serialize();
			turnOff=true;
			Controller.myMuscleScore=0;
			Application.LoadLevel(0);

		
			}
	}

	// Use this for initialization
	void Start () {
		Controller.myMuscleName = "AAA";
		Screen.showCursor = true;
	}

}
