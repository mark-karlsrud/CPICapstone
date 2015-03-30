using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabDropScript : MonoBehaviour 
{
	public List<GameObject> draggableObjects = new List<GameObject> ();
	public float dragSpeed = 3.0f;
	public Material selectedObjectMaterial;
	public GUIText infoGuiText;
	
	private InteractionManager manager;
	private bool isLeftHandDrag;
	public static bool grabbedPaddle;
	
	private GameObject draggedObject, draggedObject2,player,ball,healthBall,evilBall;
	private float draggedX, draggedX2;
	private float draggedObjectDepth;
	private Vector3 draggedObjectOffset;
	private Material draggedObjectMaterial;

	//used for mouse speed and direction
	private Vector3 handDelta = Vector3.zero;
	private Vector3 lastHandPosition = Vector3.zero;
	
	
	void Awake() 
	{
		//infoGuiText = GameObject.Find("HandGuiText");
	}

	void Start()
	{
		draggableObjects.Add (player);
		draggableObjects.Add (ball);
		draggableObjects.Add (healthBall);
		draggableObjects.Add (evilBall);

	}
	
	void Update() 
	{
		manager = InteractionManager.Instance;


		//mouse speed and direction
		Vector3 screenPos = manager.GetLeftHandScreenPos();
		Vector3 currentHandPos = new Vector3(
			(int)(screenPos.x * Camera.main.pixelWidth),
			(int)(screenPos.y * Camera.main.pixelHeight),
			0);
		handDelta = currentHandPos - lastHandPosition;
		lastHandPosition = currentHandPos;
		//Debug.Log (handDelta);


		bool finishedDeleting = false;
		while (!finishedDeleting) {
			finishedDeleting = true;
			foreach (GameObject obj in draggableObjects) {
				if (obj == null) {
					draggableObjects.Remove (obj);
					finishedDeleting = false;
					break;
				}
			}
		}

		
		if(manager != null && manager.IsInteractionInited())
		{
			Vector3 screenNormalPos = Vector3.zero;
			Vector3 screenNormalPos2 = Vector3.zero;
			Vector3 screenPixelPos = Vector3.zero;
			
			if(draggedObject == null)
			{
				// no object is currently selected or dragged.
				// if there is a hand grip, try to select the underlying object and start dragging it.
				//if(manager.IsLeftHandPrimary())
				//{
				// if the left hand is primary, check for left hand grip
				if(manager.GetLastLeftHandEvent() == InteractionManager.HandEventType.Grip)
				{
					//isLeftHandDrag = true;
					screenNormalPos = manager.GetLeftHandScreenPos();
				}
				//}
				//else if(manager.IsRightHandPrimary())
				//{
				// if the right hand is primary, check for right hand grip

				//}
				
				// check if there is an underlying object to be selected
				if(screenNormalPos != Vector3.zero)
				{
					// convert the normalized screen pos to pixel pos
					screenPixelPos.x = (int)(screenNormalPos.x * Camera.main.pixelWidth);
					screenPixelPos.y = (int)(screenNormalPos.y * Camera.main.pixelHeight);
					Ray ray = Camera.main.ScreenPointToRay(screenPixelPos);
					
					// check for underlying objects
					RaycastHit hit;
					if(Physics.Raycast(ray, out hit))
					{
						foreach(GameObject obj in draggableObjects)
						{
							if(hit.collider.gameObject == obj)
							{
								// an object was hit by the ray. select it and start drgging
								draggedObject = obj;
								draggedObjectDepth = draggedObject.transform.position.z - Camera.main.transform.position.z;
								draggedObjectOffset = hit.point - draggedObject.transform.position;
								draggedX= draggedObject.transform.position.x;
								grabbedPaddle = true;
								// set selection material
								draggedObjectMaterial = draggedObject.renderer.material;
								draggedObject.renderer.material = selectedObjectMaterial;
								break;
							}
						}
					}

				}
				
			}
			else
			{
				// continue dragging the object
				screenNormalPos =  manager.GetLeftHandScreenPos();
				
				//				// check if there is pull-gesture
								//bool isPulled = isLeftHandDrag ? manager.isIsLeftHandPull(true) : manager.IsRightHandPull(true);
				//				if(isPulled)
				//				{
				//					// set object depth to its original depth
				//					draggedObjectDepth = -Camera.main.transform.position.z;
				//				}
				
				// convert the normalized screen pos to 3D-world pos
				screenPixelPos.x = (int)(screenNormalPos.x * Camera.main.pixelWidth);
				screenPixelPos.y = (int)(screenNormalPos.y * Camera.main.pixelHeight);
				screenPixelPos.z = screenNormalPos.z + draggedObjectDepth;


				Vector3 newObjectPos = (Camera.main.ScreenToWorldPoint(screenPixelPos) - draggedObjectOffset);
				newObjectPos = new Vector3(draggedX, newObjectPos.y, 0f);
				draggedObject.transform.position = Vector3.Lerp(draggedObject.transform.position, newObjectPos, dragSpeed * Time.deltaTime);

				// check if the object (hand grip) was released
				bool isReleased = (manager.GetLastLeftHandEvent() == InteractionManager.HandEventType.Release);
				
				if(isReleased)
				{
					// restore the object's material and stop dragging the object
					draggedObject.renderer.material = draggedObjectMaterial;
					draggedObject = null;
				}
			}

			if(draggedObject2 == null)
			{
				// no object is currently selected or dragged.
				// if there is a hand grip, try to select the underlying object and start dragging it.
				//if(manager.IsLeftHandPrimary())
				//{
				// if the left hand is primary, check for left hand grip
				//}
				//else if(manager.IsRightHandPrimary())
				//{
				// if the right hand is primary, check for right hand grip
				if(manager.GetLastRightHandEvent() == InteractionManager.HandEventType.Grip)
				{
					screenNormalPos2 = manager.GetRightHandScreenPos();
				}
				//}
				
				// check if there is an underlying object to be selected
					if(screenNormalPos2 != Vector3.zero)
					{// convert the normalized screen pos to pixel pos
						screenPixelPos.x = (int)(screenNormalPos2.x * Camera.main.pixelWidth);
						screenPixelPos.y = (int)(screenNormalPos2.y * Camera.main.pixelHeight);

						RaycastHit hit;

					//Debug.Log ("camera:" + transform.position);


					Ray ray2 = Camera.main.ScreenPointToRay(screenPixelPos);

					//Debug.Log ("ray:"+ray2);

					if (Physics.SphereCast(ray2, 3.5f, out hit,50)){//transform.forward, out hit, 50)){
						foreach(GameObject obj in draggableObjects)
						{
							if(obj.collider == hit.collider&&obj.gameObject.tag != "Player"){
								// an object was hit by the ray. select it and start drgging
								obj.rigidbody.velocity = Vector3.zero;
								draggedObject2 = obj;
								draggedObjectDepth = draggedObject2.transform.position.z - Camera.main.transform.position.z;
								draggedObjectOffset = hit.point - draggedObject2.transform.position;
								//Debug.Log("Screennormalpos2");
								// set selection material
								ScoreBoard.score +=4;
								draggedObjectMaterial = draggedObject2.renderer.material;
								draggedObject2.renderer.material = selectedObjectMaterial;
								break;
							}
						}
					}

					/*
						Ray ray2 = Camera.main.ScreenPointToRay(screenPixelPos);
						
						// check for underlying objects
						RaycastHit hit2;
						if(Physics.Raycast(ray2, out hit2))
						{
							foreach(GameObject obj in draggableObjects)
							{
								if(hit2.collider.gameObject == obj)
								{
									// an object was hit by the ray. select it and start drgging
									draggedObject2 = obj;
									draggedObjectDepth = draggedObject2.transform.position.z - Camera.main.transform.position.z;
									draggedObjectOffset = hit2.point - draggedObject2.transform.position;
									Debug.Log("Screennormalpos2");
									// set selection material
									draggedObjectMaterial = draggedObject2.renderer.material;
									draggedObject2.renderer.material = selectedObjectMaterial;
									break;
								}
							}
						}
*/
					}
				
			}
			else
			{
				// continue dragging the object
				screenNormalPos2 = manager.GetRightHandScreenPos();
				
				//				// check if there is pull-gesture
				//				bool isPulled = isLeftHandDrag ? manager.IsLeftHandPull(true) : manager.IsRightHandPull(true);
				//				if(isPulled)
				//				{
				//					// set object depth to its original depth
				//					draggedObjectDepth = -Camera.main.transform.position.z;
				//				}
				
				// convert the normalized screen pos to 3D-world pos
				screenPixelPos.x = (int)(screenNormalPos2.x * Camera.main.pixelWidth);
				screenPixelPos.y = (int)(screenNormalPos2.y * Camera.main.pixelHeight);
				screenPixelPos.z = screenNormalPos2.z + draggedObjectDepth;
				
				Vector3 newObjectPos = Camera.main.ScreenToWorldPoint(screenPixelPos) - draggedObjectOffset;
				draggedObject2.transform.position = Vector3.Lerp(draggedObject2.transform.position, newObjectPos, dragSpeed * Time.deltaTime);
				
				// check if the object (hand grip) was released
				bool isReleased = (manager.GetLastRightHandEvent() == InteractionManager.HandEventType.Release);
				
				if(isReleased)
				{	handDelta = new Vector3(handDelta.x,handDelta.y,0);
					//draggedObject2.transform.rigidbody.velocity = handDelta * 3;
					draggedObject2.transform.rigidbody.collider.enabled=false;
					// restore the object's material and stop dragging the object
					draggedObject2.renderer.material = draggedObjectMaterial;
					draggedObject2 = null;

				}
			}
		}
	}
	
	void OnGUI()
	{
		if(infoGuiText != null && manager != null && manager.IsInteractionInited())
		{
			string sInfo = string.Empty;
			
			long userID = manager.GetUserID();
			if(userID != 0)
			{
				if(draggedObject != null)
					sInfo = "Dragging the " + draggedObject.name + " around.";
				else
					sInfo = "Please grab and drag an object around.";
			}
			else
			{
				KinectManager kinectManager = KinectManager.Instance;
				
				if(kinectManager && kinectManager.IsInitialized())
				{
					sInfo = "Waiting for Users...";
				}
				else
				{
					sInfo = "Kinect is not initialized. Check the log for details.";
				}
			}
			
			infoGuiText.guiText.text = sInfo;
		}
	}
	
}

