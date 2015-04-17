#pragma strict
var startPos : Vector3;
var numberOfPlatforms : int;
var distanceBetween : int;
var platform : GameObject;
var platformArray : GameObject[];
var flag : boolean;

function Start () {
	flag = false;
	platformArray = new GameObject[numberOfPlatforms];
	for(var i = 0; i < numberOfPlatforms; i++){
		var newPlatform : GameObject = GameObject.Instantiate(platform, startPos + Vector3.forward * (i * distanceBetween), Quaternion.identity);
		newPlatform.GetComponent(Rigidbody).useGravity = false;
		platformArray[i] = newPlatform;
	}
}

function Update () {
	if (flag){
		
		var fallingPlatform : GameObject;
		for (var j = 0; j < platformArray.Length; j++){
			fallingPlatform = platformArray[j];
			Wait();
			fallingPlatform.GetComponent(Rigidbody).useGravity = true;
			
		}
	}
}

function Wait() {
	yield WaitForFixedUpdate();
}