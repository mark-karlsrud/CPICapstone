using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class MuscleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

	public static bool tpose,rightBicepFlex,leftBicepFlex;
	
	
	public bool IsTPose()
	{
		if(tpose)
		{
			tpose = false;
			return true;
		}
		
		return false;
	}
    public bool RightBicepFlex()
    {
        if (rightBicepFlex)
        {
            rightBicepFlex = false;
            return true;
        }

        return false;
    }
    public bool LeftBicepFlex()
    {
        if (leftBicepFlex)
        {
            leftBicepFlex = false;
            return true;
        }

        return false;
    }
	
	public void UserDetected(long userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);

		manager.DetectGesture(userId, KinectGestures.Gestures.LeftBicepFlex);
		manager.DetectGesture(userId, KinectGestures.Gestures.RightBicepFlex);
		
		manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
		
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = "Swipe left or right to change the slides.";
		}
	}
	
	public void UserLost(long userId, int userIndex)
	{
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = string.Empty;
		}
	}
	
	public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectInterop.JointType joint, Vector3 screenPos)
	{
		// don't do anything here
	}
	
	public bool GestureCompleted (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " detected";
		if(GestureInfo != null)
		{
			GestureInfo.guiText.text = sGestureText;
		}

		if (gesture == KinectGestures.Gestures.Tpose) {
			tpose = true;
		}
        else if (gesture == KinectGestures.Gestures.RightBicepFlex)
        {
            rightBicepFlex = true;
        }
        else if (gesture == KinectGestures.Gestures.LeftBicepFlex)
        {
            leftBicepFlex = true;
        }
		
		return true;
	}
	
	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}
	
}
