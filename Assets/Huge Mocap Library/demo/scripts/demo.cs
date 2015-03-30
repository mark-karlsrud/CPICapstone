using UnityEngine;
using System.Collections;

public class demo : MonoBehaviour {

	public Animator animator;
	
	void OnGUI() {

		GUILayout.BeginVertical("box");
		if (GUILayout.Button("walk1")) {
			animator.SetTrigger("walk1");
		}
		if (GUILayout.Button("walk2")) {
			animator.SetTrigger("walk2");
		}
		if (GUILayout.Button("walk back")) {
			animator.SetTrigger("walk back");
		}
		if (GUILayout.Button("walk side")) {
			animator.SetTrigger("walk side");
		}
		if (GUILayout.Button("crouch walk")) {
			animator.SetTrigger("crouch walk");
		}
		if (GUILayout.Button("crouch walk backwards")) {
			animator.SetTrigger("crouch walk backwards");
		}
		if (GUILayout.Button("rush walk")) {
			animator.SetTrigger("rush walk");
		}
		if (GUILayout.Button("hurt walk")) {
			animator.SetTrigger("hurt walk");
		}
		if (GUILayout.Button("sexy walk")) {
			animator.SetTrigger("sexy walk");
		}
		if (GUILayout.Button("run")) {
			animator.SetTrigger("run");
		}
		if (GUILayout.Button("runstartstop")) {
			animator.SetTrigger("runstartstop");
		}
		if (GUILayout.Button("run left")) {
			animator.SetTrigger("run left");
		}
		if (GUILayout.Button("run right")) {
			animator.SetTrigger("run right");
		}
		if (GUILayout.Button("roll")) {
			animator.SetTrigger("roll");
		}
		if (GUILayout.Button("pickup")) {
			animator.SetTrigger("pickup");
		}
		GUILayout.FlexibleSpace();
		GUILayout.Box("This is just a tiny sample of the 2534 animations inside of this library.");
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
	}
}
