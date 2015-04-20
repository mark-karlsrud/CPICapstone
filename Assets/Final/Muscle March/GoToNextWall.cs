using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoToNextWall : MonoBehaviour
{

    public Transform[] Walls;
    public Queue<Transform> walls;
    private Transform target;
    NavMeshAgent agent;

    // Use this for initialization
    void Start()
    {
        walls = new Queue<Transform>(Walls);
        agent = GetComponent<NavMeshAgent>();
        target = walls.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "destination")
        {
            target = walls.Dequeue();
            Destroy(col.gameObject);
        }
    }
}
