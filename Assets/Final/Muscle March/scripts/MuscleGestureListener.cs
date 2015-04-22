using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class MuscleGestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

    public static bool tpose, rightBicepFlex, leftBicepFlex, rightBicepFlexDown, leftBicepFlexDown, doubleBicepFlex, doubleBicepFlexDown;
	
	
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
        if (KinectGestures.rightBicepFlex)//rightBicepFlex)
        {
            KinectGestures.rightBicepFlex = false;
            return true;
        }

        return false;
    }
    public bool LeftBicepFlex()
    {
        if (KinectGestures.leftBicepFlex)//leftBicepFlex)
        {
            KinectGestures.leftBicepFlex = false;// leftBicepFlex = false;
            return true;
        }

        return false;
    }
    public bool RightBicepFlexDown()
    {
        if (KinectGestures.rightBicepFlexDown)
        {
            KinectGestures.rightBicepFlexDown = false;
            return true;
        }

        return false;
    }
    public bool LeftBicepFlexDown()
    {
        if (KinectGestures.leftBicepFlex)
        {
            KinectGestures.leftBicepFlex = false;
            return true;
        }

        return false;
    }
    public bool DoubleBicepFlex()
    {
        if (doubleBicepFlex)
        {
            doubleBicepFlex = false;
            return true;
        }

        return false;
    }
    public bool DoubleBicepFlexDown()
    {
        if (doubleBicepFlexDown)
        {
            doubleBicepFlexDown = false;
            return true;
        }

        return false;
    }
	
	public void UserDetected(long userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

		//manager.DetectGesture(userId, KinectGestures.Gestures.LeftBicepFlex);
		//manager.DetectGesture(userId, KinectGestures.Gestures.RightBicepFlex);
		manager.DetectGesture(userId, KinectGestures.Gestures.Tpose);
		//manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        //manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);

        manager.DetectGesture(userId, KinectGestures.Gestures.DoubleBicepFlex);
        manager.DetectGesture(userId, KinectGestures.Gestures.DoubleBicepFlexDown);
        //manager.DetectGesture(userId, KinectGestures.Gestures.LeftBicepFlexDown);
        //manager.DetectGesture(userId, KinectGestures.Gestures.RightBicepFlexDown);
        manager.DetectGesture(userId, KinectGestures.Gestures.Running);
		
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
            //rightBicepFlex = true;
        }
        else if (gesture == KinectGestures.Gestures.LeftBicepFlex)
        {
            //leftBicepFlex = true;
        }
        else if (gesture == KinectGestures.Gestures.RightBicepFlexDown)
        {
            //rightBicepFlexDown = true;
        }
        else if (gesture == KinectGestures.Gestures.LeftBicepFlexDown)
        {
            //leftBicepFlexDown = true;
        }
        else if (gesture == KinectGestures.Gestures.DoubleBicepFlex)
        {
            doubleBicepFlex = true;
        }
        else if (gesture == KinectGestures.Gestures.DoubleBicepFlexDown)
        {
            doubleBicepFlexDown = true;
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
