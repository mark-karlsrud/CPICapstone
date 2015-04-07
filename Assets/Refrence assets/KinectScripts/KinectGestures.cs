using UnityEngine;
//using Windows.Kinect;

using System.Collections;
using System.Collections.Generic;
using System;

public class KinectGestures
{
	public interface GestureListenerInterface
	{
		// Invoked when a new user is detected and tracking starts
		// Here you can start gesture detection with KinectManager.DetectGesture()
		void UserDetected(long userId, int userIndex);
		
		// Invoked when a user is lost
		// Gestures for this user are cleared automatically, but you can free the used resources
		void UserLost(long userId, int userIndex);
		
		// Invoked when a gesture is in progress 
		void GestureInProgress(long userId, int userIndex, Gestures gesture, float progress, 
		                       KinectInterop.JointType joint, Vector3 screenPos);

		// Invoked if a gesture is completed.
		// Returns true, if the gesture detection must be restarted, false otherwise
		bool GestureCompleted(long userId, int userIndex, Gestures gesture,
		                      KinectInterop.JointType joint, Vector3 screenPos);

		// Invoked if a gesture is cancelled.
		// Returns true, if the gesture detection must be retarted, false otherwise
		bool GestureCancelled(long userId, int userIndex, Gestures gesture, 
		                      KinectInterop.JointType joint);
	}
	
	
	public enum Gestures
	{
		None = 0,
		RaiseRightHand,
		RaiseLeftHand,
		Psi,
		Tpose,
		Stop,
		Wave,
//		Click,
		SwipeLeft,
		SwipeRight,
		SwipeUp,
		SwipeDown,
//		RightHandCursor,
//		LeftHandCursor,
		ZoomOut,
		ZoomIn,
		Wheel,
		Jump,
		Squat,
		Push,
		Pull,
		//OUR ADDITIONS BELOW
		RightBicepFlex,
		LeftBicepFlex,
        Running,
        RightTurn,
        LeftTurn,
        Hurdle,
        RightBicepFlexDown,
        LeftBicepFlexDown,
        DoubleBicepFlexDown,
        DoubleBicepFlex,
        RightUpLeftDownFlex,
        LeftUpRightDownFlex
	}
	
	
	public struct GestureData
	{
		public long userId;
		public Gestures gesture;
		public int state;
		public float timestamp;
		public int joint;
		public Vector3 jointPos;
		public Vector3 screenPos;
		public float tagFloat;
		public Vector3 tagVector;
		public Vector3 tagVector2;
		public float progress;
		public bool complete;
		public bool cancelled;
		public List<Gestures> checkForGestures;
		public float startTrackingAtTime;
	}
	

	
	// Gesture related constants, variables and functions
    private const int headIndex = (int)KinectInterop.JointType.Head;

	private const int leftHandIndex = (int)KinectInterop.JointType.HandLeft;
	private const int rightHandIndex = (int)KinectInterop.JointType.HandRight;
		
	private const int leftElbowIndex = (int)KinectInterop.JointType.ElbowLeft;
	private const int rightElbowIndex = (int)KinectInterop.JointType.ElbowRight;
		
	private const int leftShoulderIndex = (int)KinectInterop.JointType.ShoulderLeft;
	private const int rightShoulderIndex = (int)KinectInterop.JointType.ShoulderRight;
	
	private const int hipCenterIndex = (int)KinectInterop.JointType.SpineBase;
	private const int shoulderCenterIndex = (int)KinectInterop.JointType.SpineShoulder;
	private const int leftHipIndex = (int)KinectInterop.JointType.HipLeft;
	private const int rightHipIndex = (int)KinectInterop.JointType.HipRight;

    private const int rightFootIndex = (int)KinectInterop.JointType.FootRight;
    private const int leftFootIndex = (int)KinectInterop.JointType.FootLeft;

    private const int leftKneeIndex = (int)KinectInterop.JointType.KneeLeft;
    private const int rightKneeIndex = (int)KinectInterop.JointType.KneeRight;

    public static bool running, turnLeft, turnRight, jumping, hurdling;
    public static float ground = 0f;

    private static float rightHandX, rightHandY, rightShoulderX, rightShoulderY, rightElbowX, rightElbowY = 0f;
    private static float leftHandX, leftHandY, leftShoulderX, leftShoulderY, leftElbowX, leftElbowY = 0f;
    private static float headX, headY = 0f;
	
	private static int[] neededJointIndexes = {
		/*leftHandIndex, rightHandIndex, leftElbowIndex, rightElbowIndex, leftShoulderIndex, rightShoulderIndex,
		hipCenterIndex, shoulderCenterIndex, leftHipIndex, rightHipIndex*/
        0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24
	};
	
	
	// Returns the list of the needed gesture joint indexes
	public static int[] GetNeededJointIndexes()
	{
		return neededJointIndexes;
	}
	
	
	
	private static void SetGestureJoint(ref GestureData gestureData, float timestamp, int joint, Vector3 jointPos)
	{
		gestureData.joint = joint;
		gestureData.jointPos = jointPos;
		gestureData.timestamp = timestamp;
		gestureData.state++;
	}
	
	private static void SetGestureCancelled(ref GestureData gestureData)
	{
		gestureData.state = 0;
		gestureData.progress = 0f;
		gestureData.cancelled = true;
	}
	
	private static void CheckPoseComplete(ref GestureData gestureData, float timestamp, Vector3 jointPos, bool isInPose, float durationToComplete)
	{
		if(isInPose)
		{
			float timeLeft = timestamp - gestureData.timestamp;
			gestureData.progress = durationToComplete > 0f ? Mathf.Clamp01(timeLeft / durationToComplete) : 1.0f;
	
			if(timeLeft >= durationToComplete)
			{
				gestureData.timestamp = timestamp;
				gestureData.jointPos = jointPos;
				gestureData.state++;
				gestureData.complete = true;
			}
		}
		else
		{
			SetGestureCancelled(ref gestureData);
		}
	}
	
	private static void SetScreenPos(long userId, ref GestureData gestureData, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		Vector3 handPos = jointsPos[rightHandIndex];
//		Vector3 elbowPos = jointsPos[rightElbowIndex];
//		Vector3 shoulderPos = jointsPos[rightShoulderIndex];
		bool calculateCoords = false;
		
		if(gestureData.joint == rightHandIndex)
		{
			if(jointsTracked[rightHandIndex] /**&& jointsTracked[rightElbowIndex] && jointsTracked[rightShoulderIndex]*/)
			{
				calculateCoords = true;
			}
		}
		else if(gestureData.joint == leftHandIndex)
		{
			if(jointsTracked[leftHandIndex] /**&& jointsTracked[leftElbowIndex] && jointsTracked[leftShoulderIndex]*/)
			{
				handPos = jointsPos[leftHandIndex];
//				elbowPos = jointsPos[leftElbowIndex];
//				shoulderPos = jointsPos[leftShoulderIndex];
				
				calculateCoords = true;
			}
		}
		
		if(calculateCoords)
		{
//			if(gestureData.tagFloat == 0f || gestureData.userId != userId)
//			{
//				// get length from shoulder to hand (screen range)
//				Vector3 shoulderToElbow = elbowPos - shoulderPos;
//				Vector3 elbowToHand = handPos - elbowPos;
//				gestureData.tagFloat = (shoulderToElbow.magnitude + elbowToHand.magnitude);
//			}
			
			if(jointsTracked[hipCenterIndex] && jointsTracked[shoulderCenterIndex] && 
				jointsTracked[leftShoulderIndex] && jointsTracked[rightShoulderIndex])
			{
				Vector3 shoulderToHips = jointsPos[shoulderCenterIndex] - jointsPos[hipCenterIndex];
				Vector3 rightToLeft = jointsPos[rightShoulderIndex] - jointsPos[leftShoulderIndex];
				
				gestureData.tagVector2.x = rightToLeft.x; // * 1.2f;
				gestureData.tagVector2.y = shoulderToHips.y; // * 1.2f;
				
				if(gestureData.joint == rightHandIndex)
				{
					gestureData.tagVector.x = jointsPos[rightShoulderIndex].x - gestureData.tagVector2.x / 2;
					gestureData.tagVector.y = jointsPos[hipCenterIndex].y;
				}
				else
				{
					gestureData.tagVector.x = jointsPos[leftShoulderIndex].x - gestureData.tagVector2.x / 2;
					gestureData.tagVector.y = jointsPos[hipCenterIndex].y;
				}
			}
	
//			Vector3 shoulderToHand = handPos - shoulderPos;
//			gestureData.screenPos.x = Mathf.Clamp01((gestureData.tagFloat / 2 + shoulderToHand.x) / gestureData.tagFloat);
//			gestureData.screenPos.y = Mathf.Clamp01((gestureData.tagFloat / 2 + shoulderToHand.y) / gestureData.tagFloat);
			
			if(gestureData.tagVector2.x != 0 && gestureData.tagVector2.y != 0)
			{
				Vector3 relHandPos = handPos - gestureData.tagVector;
				gestureData.screenPos.x = Mathf.Clamp01(relHandPos.x / gestureData.tagVector2.x);
				gestureData.screenPos.y = Mathf.Clamp01(relHandPos.y / gestureData.tagVector2.y);
			}
			
			//Debug.Log(string.Format("{0} - S: {1}, H: {2}, SH: {3}, L : {4}", gestureData.gesture, shoulderPos, handPos, shoulderToHand, gestureData.tagFloat));
		}
	}
	
	private static void SetZoomFactor(long userId, ref GestureData gestureData, float initialZoom, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
		Vector3 vectorZooming = jointsPos[rightHandIndex] - jointsPos[leftHandIndex];
		
		if(gestureData.tagFloat == 0f || gestureData.userId != userId)
		{
			gestureData.tagFloat = 0.5f; // this is 100%
		}

		float distZooming = vectorZooming.magnitude;
		gestureData.screenPos.z = initialZoom + (distZooming / gestureData.tagFloat);
	}
	
	private static void SetWheelRotation(long userId, ref GestureData gestureData, Vector3 initialPos, Vector3 currentPos)
	{
		float angle = Vector3.Angle(initialPos, currentPos) * Mathf.Sign(currentPos.y - initialPos.y);
		gestureData.screenPos.z = angle;
	}
	
	// estimate the next state and completeness of the gesture
	public static void CheckForGesture(long userId, ref GestureData gestureData, float timestamp, ref Vector3[] jointsPos, ref bool[] jointsTracked)
	{
        if (ground == 0f)
        {
            ground = jointsPos[rightFootIndex].y;
            //Debug.Log("ground:" + ground);
        }
        
        if (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] && jointsTracked[rightElbowIndex])
        {
            rightHandX = jointsPos[rightHandIndex].x;
            rightHandY = jointsPos[rightHandIndex].y;
            rightShoulderX = jointsPos[rightShoulderIndex].x;
            rightShoulderY = jointsPos[rightShoulderIndex].y;
            rightElbowX = jointsPos[rightElbowIndex].x;
            rightElbowY = jointsPos[rightElbowIndex].y;
        }
        if (jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] && jointsTracked[leftElbowIndex])
        {
            leftHandX = jointsPos[leftHandIndex].x;
            leftHandY = jointsPos[leftHandIndex].y;
            leftShoulderX = jointsPos[leftShoulderIndex].x;
            leftShoulderY = jointsPos[leftShoulderIndex].y;
            leftElbowX = jointsPos[leftElbowIndex].x;
            leftElbowY = jointsPos[leftElbowIndex].y;
        }
        if (jointsTracked[headIndex])
        {
            headX = jointsPos[headIndex].x;
            headY = jointsPos[headIndex].y;
        }


		if(gestureData.complete)
			return;
		
		switch(gestureData.gesture)
		{

			//check for right bicep flex
			//MARK ADDED THIS CASE

            // check for Running
            case Gestures.Running:

                switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
                        if (jointsTracked[rightFootIndex] && jointsTracked[leftFootIndex] &&
                            Math.Abs(jointsPos[rightFootIndex].y - jointsPos[leftFootIndex].y) > 0.1)
                        {
                            SetGestureJoint(ref gestureData, timestamp, hipCenterIndex, jointsPos[hipCenterIndex]);
                            gestureData.progress = 0.5f;
                        }
                        else
                        {
                            running = false;
                            //Debug.Log("---");
                        }
                        
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
                            bool isInPose = (jointsTracked[rightFootIndex] && jointsTracked[leftFootIndex] &&
                                Math.Abs(jointsPos[rightFootIndex].y - jointsPos[leftFootIndex].y) < 0.13);

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
                                running = true;
                                Debug.Log("running");
                                return;
							}
                            else
                            {
                                running = false;
                                //Debug.Log("---");
                            }
						}
						else
						{
							// cancel the gesture
                            //Debug.Log("---");
                            running = false;
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

            case Gestures.RightBicepFlexDown:
                switch (gestureData.state)
                {
                    case 0:  // gesture detection
                        if (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] && jointsTracked[rightElbowIndex] && jointsTracked[headIndex] &&
                           rightHandY < rightShoulderY && //right hand must be below right shoulder
                           rightElbowX > rightShoulderX && //right shoulder must be to the right of right elbow
                           Math.Abs(rightElbowY - rightShoulderY) < 0.3f && //right elbow and right shoulder should be at roughly the same height
                            rightHandY < rightElbowY) //right hand should be lower than elbow
                        {
                            SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
                        }
                        break;

                    case 1:  // gesture complete
                        bool isInPose = (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] && jointsTracked[rightElbowIndex] && jointsTracked[headIndex] &&
                           rightHandY < rightShoulderY && //right hand must be below right shoulder
                           rightElbowX > rightShoulderX && //right shoulder must be to the right of right elbow
                           Math.Abs(rightElbowY - rightShoulderY) < 0.3f && //right elbow and right shoulder should be at roughly the same height
                            rightHandY < rightElbowY); //right hand should be lower than elbow

                        Vector3 jointPos = jointsPos[gestureData.joint];
                        CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
                        break;
                }
                break;

            case Gestures.LeftBicepFlexDown:
                switch (gestureData.state)
                {
                    case 0:  // gesture detection
                        if (jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] && jointsTracked[leftElbowIndex] && jointsTracked[headIndex] &&
                           leftHandY < leftShoulderY && //left hand must be below left shoulder
                           leftElbowX < leftShoulderX && //left shoulder must be to the left of left elbow
                           Math.Abs(leftElbowY - leftShoulderY) < 0.3f && //left elbow and left shoulder should be at roughly the same height
                            leftHandY < leftElbowY) //left hand should be lower than elbow
                        {
                            SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
                        }
                        break;

                    case 1:  // gesture complete
                        bool isInPose = (jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] && jointsTracked[leftElbowIndex] && jointsTracked[headIndex] &&
                           leftHandY < leftShoulderY && //left hand must be below left shoulder
                           leftElbowX < leftShoulderX && //left shoulder must be to the left of left elbow
                           Math.Abs(leftElbowY - leftShoulderY) < 0.3f && //left elbow and left shoulder should be at roughly the same height
                            leftHandY < leftElbowY); //left hand should be lower than elbow

                        Vector3 jointPos = jointsPos[gestureData.joint];
                        CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
                        break;
                }
                break;
                
			case Gestures.RightBicepFlex:
				switch(gestureData.state)
				{
					case 0:  // gesture detection
                        if (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] && jointsTracked[rightElbowIndex] && jointsTracked[headIndex] &&
                           rightHandY > rightShoulderY && //right hand must be above right shoulder
                           rightElbowX > rightShoulderX && //right shoulder must be to the right of rright elbow
                           Math.Abs(rightElbowY - rightShoulderY) < 0.2f && //right elbow and right shoulder should be at roughly the same height
                            rightHandY > rightElbowY && //right hand should be higher than elbow
                            Math.Abs(rightHandY - headY) < 0.04f) //right hand shouldn't he way higher than head
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
						}
						break;
							
					case 1:  // gesture complete
						bool isInPose = (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] && jointsTracked[rightElbowIndex] && jointsTracked[headIndex] &&
                               rightHandY > rightShoulderY && //right hand must be above right shoulder
                               rightElbowX < rightShoulderX && //right shoulder must be to the right of rright elbow
                               Math.Abs(rightElbowY - rightShoulderY) < 0.2f && //right elbow and right shoulder should be at roughly the same height
                                rightHandY > rightElbowY && //right hand should be higher than elbow
                                Math.Abs(rightHandY - headY) < 0.04f); //right hand and right shoulder should be close laterally

						Vector3 jointPos = jointsPos[gestureData.joint];
						CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
						break;
				}
				break;

			//check for left bicep flex
			//MARK ADDED THIS CASE
			
			case Gestures.LeftBicepFlex:
				switch(gestureData.state)
				{
				case 0:  // gesture detection
					if(jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] && jointsTracked[leftElbowIndex] && jointsTracked[headIndex] &&
					   leftHandY > leftShoulderY && //left hand must be above left shoulder
					   leftElbowX < leftShoulderX && //left shoulder must be to the right of rleft elbow
					   Math.Abs(leftElbowY - leftShoulderY) < 0.2f && //left elbow and left shoulder should be at roughly the same height
					    leftHandY > leftElbowY && //left hand should be higher than elbow
                        Math.Abs(leftHandY - headY) < 0.04f) //left hand shouldn't he way higher than head
					{
						SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
					}
					break;
					
				case 1:  // gesture complete
				bool isInPose = (jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] && jointsTracked[leftElbowIndex] && jointsTracked[headIndex] &&
					   leftHandY > leftShoulderY && //left hand must be above left shoulder
					   leftElbowX < leftShoulderX && //left shoulder must be to the right of rleft elbow
					   Math.Abs(leftElbowY - leftShoulderY) < 0.2f && //left elbow and left shoulder should be at roughly the same height
					    leftHandY > leftElbowY && //left hand should be higher than elbow
                        Math.Abs(leftHandY - headY) < 0.04f);
					
					Vector3 jointPos = jointsPos[gestureData.joint];
					CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
					break;
				}
				break;

                /***** The Following two are used for turning *****/
            case Gestures.RightTurn:
                switch (gestureData.state)
                {
                    case 0:  // gesture detection
                        if (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] &&
                           (jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.1f)
                        {
                            SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
                        }
                        turnRight = false;
                        break;

                    case 1:  // gesture complete
                        bool isInPose = jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] &&
                            (jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.1f;
                        turnRight = isInPose;

                        break;
                }
                break;

            // check for RaiseLeftHand
            case Gestures.LeftTurn:
                switch (gestureData.state)
                {
                    case 0:  // gesture detection
                        if (jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] &&
                                (jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.1f)
                        {
                            SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
                        }
                        turnLeft = false;
                        break;

                    case 1:  // gesture complete
                        bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] &&
                            (jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.1f;
                        turnLeft = isInPose;
                        break;
                }
                break;

			// check for RaiseRightHand
			case Gestures.RaiseRightHand:
				switch(gestureData.state)
				{
					case 0:  // gesture detection
                        if (jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] &&
                           (jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.1f)
                        {
                            SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
                        }
						break;
							
					case 1:  // gesture complete
						bool isInPose = jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] &&
							(jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.1f;

						Vector3 jointPos = jointsPos[gestureData.joint];
						CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
						break;
				}
				break;

			// check for RaiseLeftHand
			case Gestures.RaiseLeftHand:
				switch(gestureData.state)
				{
					case 0:  // gesture detection
						if(jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.1f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
						}
						break;
							
					case 1:  // gesture complete
						bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] &&
							(jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.1f;

						Vector3 jointPos = jointsPos[gestureData.joint];
						CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
						break;
				}
				break;

			// check for Psi
			case Gestures.Psi:
				switch(gestureData.state)
				{
					case 0:  // gesture detection
						if(jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.1f &&
					       jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] &&
					       (jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.1f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
						}
						break;
							
					case 1:  // gesture complete
						bool isInPose = jointsTracked[rightHandIndex] && jointsTracked[rightShoulderIndex] &&
							(jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.1f &&
							jointsTracked[leftHandIndex] && jointsTracked[leftShoulderIndex] &&
							(jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.1f;

						Vector3 jointPos = jointsPos[gestureData.joint];
						CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
						break;
				}
				break;

			// check for Tpose
			case Gestures.Tpose:
				switch(gestureData.state)
				{
					case 0:  // gesture detection
						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[rightShoulderIndex] &&
					       Mathf.Abs(jointsPos[rightElbowIndex].y - jointsPos[rightShoulderIndex].y) < 0.1f &&  // 0.07f
					       Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) < 0.1f &&  // 0.7f
					   	   jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[leftShoulderIndex] &&
					  	   Mathf.Abs(jointsPos[leftElbowIndex].y - jointsPos[leftShoulderIndex].y) < 0.1f &&
					       Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) < 0.1f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
						}
						break;
						
					case 1:  // gesture complete
						bool isInPose = jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[rightShoulderIndex] &&
								Mathf.Abs(jointsPos[rightElbowIndex].y - jointsPos[rightShoulderIndex].y) < 0.1f &&  // 0.7f
							    Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightShoulderIndex].y) < 0.1f &&  // 0.7f
							    jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[leftShoulderIndex] &&
								Mathf.Abs(jointsPos[leftElbowIndex].y - jointsPos[leftShoulderIndex].y) < 0.1f &&
							    Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftShoulderIndex].y) < 0.1f;
						
						Vector3 jointPos = jointsPos[gestureData.joint];
						CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
						break;
				}
				break;
				
			// check for Stop
			case Gestures.Stop:
				switch(gestureData.state)
				{
					case 0:  // gesture detection
						if(jointsTracked[rightHandIndex] && jointsTracked[rightHipIndex] &&
					       Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightHipIndex].y) < 0.2f &&
				   		   (jointsPos[rightHandIndex].x - jointsPos[rightHipIndex].x) >= 0.4f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
						}
						else if(jointsTracked[leftHandIndex] && jointsTracked[leftHipIndex] &&
					       Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftHipIndex].y) < 0.2f &&
				           (jointsPos[leftHandIndex].x - jointsPos[leftHipIndex].x) <= -0.4f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
						}
						break;
							
					case 1:  // gesture complete
						bool isInPose = (gestureData.joint == rightHandIndex) ?
							(jointsTracked[rightHandIndex] && jointsTracked[rightHipIndex] &&
							Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightHipIndex].y) < 0.2f &&
				 			(jointsPos[rightHandIndex].x - jointsPos[rightHipIndex].x) >= 0.4f) :
							(jointsTracked[leftHandIndex] && jointsTracked[leftHipIndex] &&
							Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftHipIndex].y) < 0.2f &&
						 	(jointsPos[leftHandIndex].x - jointsPos[leftHipIndex].x) <= -0.4f);

						Vector3 jointPos = jointsPos[gestureData.joint];
						CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.PoseCompleteDuration);
						break;
				}
				break;

			// check for Wave
			case Gestures.Wave:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f &&
					       (jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) > 0.05f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.3f;
						}
						else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
					            (jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < -0.05f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
							gestureData.progress = 0.3f;
						}
						break;
				
					case 1:  // gesture - phase 2
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f && 
								(jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) < -0.05f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
								(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) > 0.05f;
				
							if(isInPose)
							{
								gestureData.timestamp = timestamp;
								gestureData.state++;
								gestureData.progress = 0.7f;
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
									
					case 2:  // gesture phase 3 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f && 
								(jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) > 0.05f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
								(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < -0.05f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

//			// check for Click
//			case Gestures.Click:
//				switch(gestureData.state)
//				{
//					case 0:  // gesture detection - phase 1
//						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
//					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f)
//						{
//							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
//							gestureData.progress = 0.3f;
//
//							// set screen position at the start, because this is the most accurate click position
//							SetScreenPos(userId, ref gestureData, ref jointsPos, ref jointsTracked);
//						}
//						else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
//					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f)
//						{
//							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
//							gestureData.progress = 0.3f;
//
//							// set screen position at the start, because this is the most accurate click position
//							SetScreenPos(userId, ref gestureData, ref jointsPos, ref jointsTracked);
//						}
//						break;
//				
//					case 1:  // gesture - phase 2
////						if((timestamp - gestureData.timestamp) < 1.0f)
////						{
////							bool isInPose = gestureData.joint == rightHandIndex ?
////								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
////								//(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f && 
////								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.08f &&
////								(jointsPos[rightHandIndex].z - gestureData.jointPos.z) < -0.05f :
////								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
////								//(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
////								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.08f &&
////								(jointsPos[leftHandIndex].z - gestureData.jointPos.z) < -0.05f;
////				
////							if(isInPose)
////							{
////								gestureData.timestamp = timestamp;
////								gestureData.jointPos = jointsPos[gestureData.joint];
////								gestureData.state++;
////								gestureData.progress = 0.7f;
////							}
////							else
////							{
////								// check for stay-in-place
////								Vector3 distVector = jointsPos[gestureData.joint] - gestureData.jointPos;
////								isInPose = distVector.magnitude < 0.05f;
////
////								Vector3 jointPos = jointsPos[gestureData.joint];
////								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, Constants.ClickStayDuration);
////							}
////						}
////						else
//						{
//							// check for stay-in-place
//							Vector3 distVector = jointsPos[gestureData.joint] - gestureData.jointPos;
//							bool isInPose = distVector.magnitude < 0.05f;
//
//							Vector3 jointPos = jointsPos[gestureData.joint];
//							CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, KinectInterop.Constants.ClickStayDuration);
////							SetGestureCancelled(gestureData);
//						}
//						break;
//									
////					case 2:  // gesture phase 3 = complete
////						if((timestamp - gestureData.timestamp) < 1.0f)
////						{
////							bool isInPose = gestureData.joint == rightHandIndex ?
////								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
////								//(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.1f && 
////								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.08f &&
////								(jointsPos[rightHandIndex].z - gestureData.jointPos.z) > 0.05f :
////								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
////								//(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.1f &&
////								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.08f &&
////								(jointsPos[leftHandIndex].z - gestureData.jointPos.z) > 0.05f;
////
////							if(isInPose)
////							{
////								Vector3 jointPos = jointsPos[gestureData.joint];
////								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
////							}
////						}
////						else
////						{
////							// cancel the gesture
////							SetGestureCancelled(ref gestureData);
////						}
////						break;
//				}
//				break;

			// check for SwipeLeft
			case Gestures.SwipeLeft:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.05f &&
					       (jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) > 0f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.5f;
						}
//						else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
//					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.05f &&
//					            (jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) > 0f)
//						{
//							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
//							//gestureData.jointPos = jointsPos[leftHandIndex];
//							gestureData.progress = 0.5f;
//						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) < 0.1f && 
								Mathf.Abs(jointsPos[rightHandIndex].y - gestureData.jointPos.y) < 0.08f && 
								(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < -0.15f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) < 0.1f &&
								Mathf.Abs(jointsPos[leftHandIndex].y - gestureData.jointPos.y) < 0.08f && 
								(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < -0.15f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for SwipeRight
			case Gestures.SwipeRight:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
//						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
//					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.05f &&
//					       (jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) < 0f)
//						{
//							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
//							//gestureData.jointPos = jointsPos[rightHandIndex];
//							gestureData.progress = 0.5f;
//						}
//						else 
						if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.05f &&
					            (jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < 0f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								Mathf.Abs(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) < 0.1f && 
								Mathf.Abs(jointsPos[rightHandIndex].y - gestureData.jointPos.y) < 0.08f && 
								(jointsPos[rightHandIndex].x - gestureData.jointPos.x) > 0.15f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								Mathf.Abs(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) < 0.1f &&
								Mathf.Abs(jointsPos[leftHandIndex].y - gestureData.jointPos.y) < 0.08f && 
								(jointsPos[leftHandIndex].x - gestureData.jointPos.x) > 0.15f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for SwipeUp
			case Gestures.SwipeUp:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) < -0.05f &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.15f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.5f;
						}
						else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) < -0.05f &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.15f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] && jointsTracked[leftShoulderIndex] &&
								//(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0.1f && 
								//(jointsPos[rightHandIndex].y - gestureData.jointPos.y) > 0.15f && 
								(jointsPos[rightHandIndex].y - jointsPos[leftShoulderIndex].y) > 0.05f && 
								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.08f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] && jointsTracked[rightShoulderIndex] &&
								//(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0.1f &&
								//(jointsPos[leftHandIndex].y - gestureData.jointPos.y) > 0.15f && 
								(jointsPos[leftHandIndex].y - jointsPos[rightShoulderIndex].y) > 0.05f && 
								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.08f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for SwipeDown
			case Gestures.SwipeDown:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[leftShoulderIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[leftShoulderIndex].y) >= 0.05f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.5f;
						}
						else if(jointsTracked[leftHandIndex] && jointsTracked[rightShoulderIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[rightShoulderIndex].y) >= 0.05f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								//(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) < -0.1f && 
								(jointsPos[rightHandIndex].y - gestureData.jointPos.y) < -0.2f && 
								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.08f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								//(jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) < -0.1f &&
								(jointsPos[leftHandIndex].y - gestureData.jointPos.y) < -0.2f && 
								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.08f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

//			// check for RightHandCursor
//			case Gestures.RightHandCursor:
//				switch(gestureData.state)
//				{
//					case 0:  // gesture detection - phase 1 (perpetual)
//						if(jointsTracked[rightHandIndex] && jointsTracked[rightHipIndex] &&
//							//(jointsPos[rightHandIndex].y - jointsPos[rightHipIndex].y) > -0.1f)
//				   			(jointsPos[rightHandIndex].y - jointsPos[hipCenterIndex].y) >= 0f)
//						{
//							gestureData.joint = rightHandIndex;
//							gestureData.timestamp = timestamp;
//							gestureData.jointPos = jointsPos[rightHandIndex];
//
//							SetScreenPos(userId, ref gestureData, ref jointsPos, ref jointsTracked);
//							gestureData.progress = 0.7f;
//						}
//						else
//						{
//							// cancel the gesture
//							//SetGestureCancelled(ref gestureData);
//							gestureData.progress = 0f;
//						}
//						break;
//				
//				}
//				break;
//
//			// check for LeftHandCursor
//			case Gestures.LeftHandCursor:
//				switch(gestureData.state)
//				{
//					case 0:  // gesture detection - phase 1 (perpetual)
//						if(jointsTracked[leftHandIndex] && jointsTracked[leftHipIndex] &&
//							//(jointsPos[leftHandIndex].y - jointsPos[leftHipIndex].y) > -0.1f)
//							(jointsPos[leftHandIndex].y - jointsPos[hipCenterIndex].y) >= 0f)
//						{
//							gestureData.joint = leftHandIndex;
//							gestureData.timestamp = timestamp;
//							gestureData.jointPos = jointsPos[leftHandIndex];
//
//							SetScreenPos(userId, ref gestureData, ref jointsPos, ref jointsTracked);
//							gestureData.progress = 0.7f;
//						}
//						else
//						{
//							// cancel the gesture
//							//SetGestureCancelled(ref gestureData);
//							gestureData.progress = 0f;
//						}
//						break;
//				
//				}
//				break;

			// check for ZoomOut
			case Gestures.ZoomOut:
				Vector3 vectorZoomOut = (Vector3)jointsPos[rightHandIndex] - jointsPos[leftHandIndex];
				float distZoomOut = vectorZoomOut.magnitude;
			
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
						   jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0f &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0f &&
						   distZoomOut < 0.2f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.tagVector = Vector3.right;
							gestureData.tagFloat = 0f;
							gestureData.progress = 0.3f;
						}
						break;
				
					case 1:  // gesture phase 2 = zooming
						if((timestamp - gestureData.timestamp) < 1.0f)
						{
							float angleZoomOut = Vector3.Angle(gestureData.tagVector, vectorZoomOut) * Mathf.Sign(vectorZoomOut.y - gestureData.tagVector.y);
							bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					   			jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								((jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0f ||
				       			(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0f) &&
								distZoomOut < 1.5f && Mathf.Abs(angleZoomOut) < 20f;

							if(isInPose)
							{
								SetZoomFactor(userId, ref gestureData, 1.0f, ref jointsPos, ref jointsTracked);
								gestureData.timestamp = timestamp;
								gestureData.progress = 0.7f;
							}
//							else
//							{
//								// cancel the gesture
//								SetGestureCancelled(ref gestureData);
//							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for ZoomIn
			case Gestures.ZoomIn:
				Vector3 vectorZoomIn = (Vector3)jointsPos[rightHandIndex] - jointsPos[leftHandIndex];
				float distZoomIn = vectorZoomIn.magnitude;
				
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
						   jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0f &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0f &&
						   distZoomIn >= 0.7f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.tagVector = Vector3.right;
							gestureData.tagFloat = distZoomIn;
							gestureData.progress = 0.3f;
						}
						break;
				
					case 1:  // gesture phase 2 = zooming
						if((timestamp - gestureData.timestamp) < 1.0f)
						{
							float angleZoomIn = Vector3.Angle(gestureData.tagVector, vectorZoomIn) * Mathf.Sign(vectorZoomIn.y - gestureData.tagVector.y);
							bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					   			jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								((jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0f ||
				       			(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0f) &&
								distZoomIn >= 0.1f && Mathf.Abs(angleZoomIn) < 20f;

							if(isInPose)
							{
								SetZoomFactor(userId, ref gestureData, 0.0f, ref jointsPos, ref jointsTracked);
								gestureData.timestamp = timestamp;
								gestureData.progress = 0.7f;
							}
//							else
//							{
//								// cancel the gesture
//								SetGestureCancelled(ref gestureData);
//							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for Wheel
			case Gestures.Wheel:
				Vector3 vectorWheel = (Vector3)jointsPos[rightHandIndex] - jointsPos[leftHandIndex];
				float distWheel = vectorWheel.magnitude;

//				Debug.Log(string.Format("{0}. Dist: {1:F1}, Tag: {2:F1}, Diff: {3:F1}", gestureData.state,
//				                        distWheel, gestureData.tagFloat, Mathf.Abs(distWheel - gestureData.tagFloat)));

				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
						   jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0f &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0f &&
						   distWheel >= 0.2f && distWheel < 0.7f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.tagVector = Vector3.right;
							gestureData.tagFloat = distWheel;
							gestureData.progress = 0.3f;
						}
						break;
				
					case 1:  // gesture phase 2 = zooming
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					   			jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								((jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > 0f ||
				       			(jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > 0f) &&
								distWheel >= 0.2f && distWheel < 0.7f && 
								Mathf.Abs(distWheel - gestureData.tagFloat) < 0.1f;

							if(isInPose)
							{
								SetWheelRotation(userId, ref gestureData, gestureData.tagVector, vectorWheel);
								gestureData.timestamp = timestamp;
								gestureData.tagFloat = distWheel;
								gestureData.progress = 0.7f;
							}
//							else
//							{
//								// cancel the gesture
//								SetGestureCancelled(ref gestureData);
//							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

            // check for hurdle
            case Gestures.Hurdle:
                        if (jointsTracked[rightFootIndex] && jointsTracked[leftFootIndex] &&
                                jointsPos[rightFootIndex].y > ground + 0.15f
                            )
                        {
                            hurdling = true;
                        }
                        else
                        {
                            hurdling = false;
                        }
						break;
			
			// check for Jump
			case Gestures.Jump:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[hipCenterIndex])// && 
							//(jointsPos[hipCenterIndex].y > 0.8f) && (jointsPos[hipCenterIndex].y < 1.3f)) //Standing
						{
							SetGestureJoint(ref gestureData, timestamp, hipCenterIndex, jointsPos[hipCenterIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = jointsTracked[hipCenterIndex] &&
								(jointsPos[hipCenterIndex].y - gestureData.jointPos.y) > 0.1f && 
								Mathf.Abs(jointsPos[hipCenterIndex].x - gestureData.jointPos.x) < 0.1f;

                            jumping = isInPose;
							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
                            jumping = false;
						}
						break;
				}
				break;

			// check for Squat
			case Gestures.Squat:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[hipCenterIndex] && 
							(jointsPos[hipCenterIndex].y < 0.8f))
						{
							SetGestureJoint(ref gestureData, timestamp, hipCenterIndex, jointsPos[hipCenterIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = jointsTracked[hipCenterIndex] &&
								(jointsPos[hipCenterIndex].y - gestureData.jointPos.y) < -0.15f && 
								Mathf.Abs(jointsPos[hipCenterIndex].x - gestureData.jointPos.x) < 0.15f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for Push
			case Gestures.Push:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.05f &&
					       Mathf.Abs(jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) < 0.15f &&
						   (jointsPos[rightHandIndex].z - jointsPos[rightElbowIndex].z) < -0.05f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.5f;
						}
						else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.05f &&
					            Mathf.Abs(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < 0.15f &&
							    (jointsPos[leftHandIndex].z - jointsPos[leftElbowIndex].z) < -0.05f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.15f && 
								Mathf.Abs(jointsPos[rightHandIndex].y - gestureData.jointPos.y) < 0.15f && 
								(jointsPos[rightHandIndex].z - gestureData.jointPos.z) < -0.15f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.15f &&
								Mathf.Abs(jointsPos[leftHandIndex].y - gestureData.jointPos.y) < 0.15f && 
								(jointsPos[leftHandIndex].z - gestureData.jointPos.z) < -0.15f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// check for Pull
			case Gestures.Pull:
				switch(gestureData.state)
				{
					case 0:  // gesture detection - phase 1
						if(jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
					       (jointsPos[rightHandIndex].y - jointsPos[rightElbowIndex].y) > -0.05f &&
					       Mathf.Abs(jointsPos[rightHandIndex].x - jointsPos[rightElbowIndex].x) < 0.15f &&
						   (jointsPos[rightHandIndex].z - jointsPos[rightElbowIndex].z) < -0.15f)
						{
							SetGestureJoint(ref gestureData, timestamp, rightHandIndex, jointsPos[rightHandIndex]);
							gestureData.progress = 0.5f;
						}
						else if(jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
					            (jointsPos[leftHandIndex].y - jointsPos[leftElbowIndex].y) > -0.05f &&
					            Mathf.Abs(jointsPos[leftHandIndex].x - jointsPos[leftElbowIndex].x) < 0.15f &&
							    (jointsPos[leftHandIndex].z - jointsPos[leftElbowIndex].z) < -0.15f)
						{
							SetGestureJoint(ref gestureData, timestamp, leftHandIndex, jointsPos[leftHandIndex]);
							gestureData.progress = 0.5f;
						}
						break;
				
					case 1:  // gesture phase 2 = complete
						if((timestamp - gestureData.timestamp) < 1.5f)
						{
							bool isInPose = gestureData.joint == rightHandIndex ?
								jointsTracked[rightHandIndex] && jointsTracked[rightElbowIndex] &&
								Mathf.Abs(jointsPos[rightHandIndex].x - gestureData.jointPos.x) < 0.15f && 
								Mathf.Abs(jointsPos[rightHandIndex].y - gestureData.jointPos.y) < 0.15f && 
								(jointsPos[rightHandIndex].z - gestureData.jointPos.z) > 0.15f :
								jointsTracked[leftHandIndex] && jointsTracked[leftElbowIndex] &&
								Mathf.Abs(jointsPos[leftHandIndex].x - gestureData.jointPos.x) < 0.15f &&
								Mathf.Abs(jointsPos[leftHandIndex].y - gestureData.jointPos.y) < 0.15f && 
								(jointsPos[leftHandIndex].z - gestureData.jointPos.z) > 0.15f;

							if(isInPose)
							{
								Vector3 jointPos = jointsPos[gestureData.joint];
								CheckPoseComplete(ref gestureData, timestamp, jointPos, isInPose, 0f);
							}
						}
						else
						{
							// cancel the gesture
							SetGestureCancelled(ref gestureData);
						}
						break;
				}
				break;

			// here come more gesture-cases

            
		}
	}

}
