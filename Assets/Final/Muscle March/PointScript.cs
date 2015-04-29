using UnityEngine;
using System.Collections;
//using UnityEngine.UI;

public class PointScript : MonoBehaviour {

    public GUIText scoreText;
    private int score;
    private CardioScript cardio;
    private Animator animator;

	// Use this for initialization
	void Start () {
        score = 0;
        cardio = GetComponent<CardioScript>();
        animator = GetComponent<Animator>();
	}

    void Awake()
    {
        score = 0;
    }

    public void rightPose()
    {
        score += 100;
    }
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();
        Controller.myMuscleScore = score;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "wall collider")
        {
            score += cardio.getCardio();
        }
    }
}
