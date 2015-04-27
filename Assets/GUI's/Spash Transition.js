#pragma strict

var levelToLoad : String;
var quitButton : boolean = false;
function Start () {
	yield new WaitForSeconds (5.0);
	if(quitButton) {
		Application.Quit();
		Debug.Log("this will work when published to the desktop");
	}
	else {
		Application.LoadLevel(levelToLoad);
	}

}
