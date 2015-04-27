using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PointScript : MonoBehaviour {

    public Text scoreText;
    private int score;
    private CardioScript cardio;
    private Animator animator;

	// Use this for initialization
	void Start () {
        cardio = GetComponent<CardioScript>();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = score.ToString();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "wall collider")
        {
            score += cardio.getCardio();

            if (animator.GetCurrentAnimatorStateInfo(3).IsName(col.gameObject.name))
            {
                Debug.Log("right pose");
                score += 100;
            }
            else
            {
                Debug.Log("wrong pose");
            }
        }
    }
}
