using UnityEngine;
using System.Collections;
using System;
using System.Timers;
//using Windows.Kinect;

public class RunningListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
    // GUI Text to display the gesture messages.
    public GUIText GestureInfo;

    private bool running,leftTurn,rightTurn;
    public bool hurdling;

    public bool IsRunning()
    {
        if (running)
        {
            running = false;
            return true;
        }

        return false;
    }
    public bool IsHurdling()
    {
        if (hurdling)
        {
            hurdling = false;
            return true;
        }

        return false;
    }
    public bool IsRightTurn()
    {
        if (rightTurn)
        {
            rightTurn = false;
            return true;
        }

        return false;
    }
    public bool IsLeftTurn()
    {
        if (leftTurn)
        {
            leftTurn = false;
            return true;
        }

        return false;
    }

    public void UserDetected(long userId, int userIndex)
    {
        // detect these user specific gestures
        KinectManager manager = KinectManager.Instance;

        manager.DetectGesture(userId, KinectGestures.Gestures.Running);
        //manager.DetectGesture(userId, KinectGestures.Gestures.Hurdle);
        manager.DetectGesture(userId, KinectGestures.Gestures.RightTurn);
        manager.DetectGesture(userId, KinectGestures.Gestures.LeftTurn);
        
        if (GestureInfo != null)
        {
            GestureInfo.guiText.text = "Run!";
        }
    }

    public void UserLost(long userId, int userIndex)
    {
        if (GestureInfo != null)
        {
            GestureInfo.guiText.text = "USER LOST";
        }
        KinectGestures.running = false;
    }

    public void GestureInProgress(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  float progress, KinectInterop.JointType joint, Vector3 screenPos)
    {
        // don't do anything here
    }

    public bool GestureCompleted(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint, Vector3 screenPos)
    {
        string sGestureText = gesture + " detected";



        if (GestureInfo != null)
        {
            GestureInfo.guiText.text = sGestureText;
        }
        
        if (gesture == KinectGestures.Gestures.Running)
        {
            //running = true;
        }

        if (gesture == KinectGestures.Gestures.Hurdle)
        {
            //hurdling = true;
        }

        if (gesture == KinectGestures.Gestures.RaiseRightHand)
        {
            //rightTurn = true;
            return false;
        }

        if (gesture == KinectGestures.Gestures.RaiseLeftHand)
        {
            //leftTurn = true;
            return false;
        }

        return true;
    }

    public bool GestureCancelled(long userId, int userIndex, KinectGestures.Gestures gesture,
                                  KinectInterop.JointType joint)
    {
        // don't do anything here, just reset the gesture state
        return true;
    }
}