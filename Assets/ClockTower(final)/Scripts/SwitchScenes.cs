using UnityEngine;
using System.Collections;

public class SwitchScenes : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag =="Player")
		{
			Application.LoadLevel("ClockTLeaderB");
			
		}
	}
}
