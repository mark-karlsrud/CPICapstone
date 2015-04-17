using UnityEngine;
using System.Collections;

public class MinuteTimer : MonoBehaviour {
    public float timer;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        

        timer = Time.timeSinceLevelLoad;
         Debug.Log(timer);
	
	}

    void OnGUI()
    {
       
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);

        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        GUI.Label(new Rect(10, 10, 250, 100), niceTime);
    }
}
