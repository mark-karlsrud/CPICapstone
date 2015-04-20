using UnityEngine;
using System.Collections;

public class CardioScriptOld : MonoBehaviour {

    private int cardio;
    public GUIText cardioText;
    public int maxCardio = 1000;
    public GUITexture cardioBar;
    private float fullWidth;

	// Use this for initialization
	void Start () {
        cardio = maxCardio;
        fullWidth = cardioBar.pixelInset.width;
	}
	
	// Update is called once per frame
	void Update () {
        
        if (MuscelPresentationScript.timer.Enabled)
            cardio++;
            
        else
            cardio--;

        if (cardio > maxCardio)
            cardio = maxCardio;
        else if (cardio <= 0)
        {
            cardio = 0;
            //Debug.Log("Dead");
        }

        cardioText.text = string.Format("Cardio:{0}",cardio);
        Rect rect = cardioBar.pixelInset;
        rect.width = (cardio / (float)maxCardio) * fullWidth;
        cardioBar.pixelInset = rect;
	}
}
