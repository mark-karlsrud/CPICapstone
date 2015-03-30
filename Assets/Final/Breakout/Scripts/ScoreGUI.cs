using UnityEngine;
using System.Collections;

public class ScoreGUI : MonoBehaviour {
	
	public GUISkin mane_skin;
	public GUISkin score_skin;
	public Texture2D background;
	int print_height = 75;
	int print_width = 150;

	//scalling matrix
	public float native_width = 800;
	public float native_height = 600;
	public float width_scale;
	public float height_scale;
	
	void Awake()
	{
		width_scale = Screen.width/native_width;
		height_scale = Screen.height/native_height;
	}

	void OnGUI()
	{
		GUI.matrix = Matrix4x4.TRS(new Vector3(0,0,0),
		                           Quaternion.identity,
		                           new Vector3(width_scale,height_scale,1));

		GUI.skin = mane_skin;
		GUI.DrawTexture(new Rect(0,0,native_width,native_height),background);

		GUI.Label(new Rect(native_width/2-250, print_height, 200, 50), "High Score:");
		GUI.skin = score_skin;
		print_height = print_height + 50;
		for(int i=0; i<10; i++)
		{
			GUI.Label(new Rect(print_width, print_height, 35, 20), (i+1).ToString()+".");
			GUI.Label(new Rect(print_width + 35, print_height, 95, 20), Controller.highscore[i].ToString());
			GUI.Label(new Rect(print_width + 135, print_height, 300, 20), Controller.names[i]);
			//GUI.Label(new Rect(print_width + 130, print_height, 300, 20), "012345678912345678901234567890123456789");
			print_height = print_height + 20;
		}
		if (GUI.Button (new Rect (native_width/2-165, print_height, 250, 50), "Main Menu"))
		{
			Application.LoadLevel(1);
		}
		print_height = 75;
		//GUI.Label(new Rect(50, 50, 400, 200), Controller.names[4]);
	}
}
