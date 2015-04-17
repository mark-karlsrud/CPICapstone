using UnityEngine;
using System.Collections;
using System;
//using Windows.Kinect;

public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;

    private bool tpose, swipeL, swipeR, raiseR, raiseL, Rflex, Lflex;
	
	
	public bool IsTpose()
	{
		if(tpose)
		{
			tpose = false;
			return true;
		}
		
		return false;
	}
    public bool IsSwipeLeft()
    {
        if (swipeL)
        {
            swipeL = false;
            return true;
        }

        return false;
    }
    public bool IsSwipeRight()
    {
        if (swipeR)
        {
            swipeR = false;
            return true;
        }

        return false;
    }
    public bool IsSwipeUp()
    {
        return false;
    }
    public bool IsSwipeDown()
    {
        return false;
    }
    public bool IsRaiseR()
    {
        if (raiseR)
        {
            raiseR = false;
            return true;
        }

        return false;
    }
    public bool IsRaiseL()
    {
        if (raiseL)
        {
            raiseL = false;
            return true;
        }

        return false;
    }
    public bool IsRflex()
    {
        if (Rflex)
        {
            Rflex = false;
            return true;
        }

        return false;
    }
    public bool IsLflex()
    {
        if (Lflex)
        {
            tpose = false;
            return true;
        }

        return false;
    }
	

	public void UserDetected(long userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;

        
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeLeft);
		manager.DetectGesture(userId, KinectGestures.Gestures.SwipeRight);
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
		
		if(gesture == KinectGestures.Gestures.SwipeLeft)
			swipeL = true;
		else if(gesture == KinectGestures.Gestures.SwipeRight)
			swipeR = true;
		else if (gesture == KinectGestures.Gestures.Tpose)
            tpose = true;
        else if (gesture == KinectGestures.Gestures.RaiseLeftHand)
            raiseL = true;
        else if (gesture == KinectGestures.Gestures.RaiseRightHand)
            raiseR = true;
        else if (gesture == KinectGestures.Gestures.RightBicepFlex)
            Rflex = true;
        else if (gesture == KinectGestures.Gestures.LeftBicepFlex)
            Lflex = true;
		
		return true;
	}

	public bool GestureCancelled (long userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectInterop.JointType joint)
	{
		// don't do anything here, just reset the gesture state
		return true;
	}
	
}
