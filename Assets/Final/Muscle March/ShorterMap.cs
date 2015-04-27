using UnityEngine;
using System.Collections;

public class ShorterMap : MonoBehaviour {

    public bool shorter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider col)
    {
        if (shorter && col.gameObject.name == "6")
        {
            GoToNextWall goTo = GetComponent<GoToNextWall>();
            while (goTo.walls.Peek().gameObject.name != "30")
            {
                goTo.walls.Dequeue();
            }
            goTo.setTarget();
            NavMeshAgent n = GetComponent<NavMeshAgent>();
            n.enabled = false;
            transform.position = GameObject.Find("30").transform.position;
            n.enabled = true;
        }
        if (shorter && col.gameObject.name == "63")
        {
            GoToNextWall goTo = GetComponent<GoToNextWall>();
            while (goTo.walls.Peek().gameObject.name != "69")
            {
                goTo.walls.Dequeue();
            }
            goTo.setTarget();
            NavMeshAgent n = GetComponent<NavMeshAgent>();
            n.enabled = false;
            transform.position = GameObject.Find("69").transform.position;
            n.enabled = true;
        }
        if (shorter && col.gameObject.name == "70")
        {
            GoToNextWall goTo = GetComponent<GoToNextWall>();
            while (goTo.walls.Peek().gameObject.name != "95")
            {
                goTo.walls.Dequeue();
            }
            goTo.setTarget();
            NavMeshAgent n = GetComponent<NavMeshAgent>();
            n.enabled = false;
            transform.position = GameObject.Find("95").transform.position;
            n.enabled = true;
        }
    }
}
