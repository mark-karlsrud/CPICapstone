using UnityEngine;
using System.Collections;

public class CardioScript : MonoBehaviour {

    private int cardio;
    public GameObject GUIBar;
    private GUIBarScript script;
    public int maxCardio = 1000;
    //public GUITexture cardioBar;
    private float fullWidth;
    public Camera camera;
    private MuscelPresentationScript presentationScript;

	// Use this for initialization
	void Start () {
        script = GUIBar.GetComponent<GUIBarScript>();
        presentationScript = camera.GetComponent<MuscelPresentationScript>();
        cardio = maxCardio;
        //fullWidth = cardioBar.pixelInset.width;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.M) || presentationScript.isRunning)
            cardio++;
        else
            cardio--;
        script.SetNewValue(cardio,maxCardio);

        if (cardio > maxCardio)
            cardio = maxCardio;
        else if (cardio <= 0)
        {
            cardio = 0;
            Debug.Log("Dead");
        }

        //Rect rect = cardioBar.pixelInset;
        //rect.width = (cardio / (float)maxCardio) * fullWidth;
        //cardioBar.pixelInset = rect;
	}

    public int getCardio()
    {
        Debug.Log((int)(100 * cardio / (float)maxCardio));
        return (int)(100 * cardio/(float)maxCardio);
    }
}
