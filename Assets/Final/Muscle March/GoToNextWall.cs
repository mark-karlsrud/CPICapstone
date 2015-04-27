using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GoToNextWall : MonoBehaviour
{
    public GameObject[] wallPossibilities;
    public Queue<Transform> walls;
    private Transform target;
    NavMeshAgent agent;
    public int startAtWall = -1;

    // Use this for initialization
    void Start()
    {
        //First, randomly place walls
        GameObject wallParent = GameObject.Find("Walls");
        List<Transform> wallList = new List<Transform>();
        foreach (Transform wall in wallParent.transform)
        {
            GameObject newObject;
            newObject = wallPossibilities[Random.Range(0, wallPossibilities.Length)];
            newObject.transform.position = wall.transform.position;
            newObject.transform.rotation = wall.transform.rotation;
            newObject.transform.localScale = wall.transform.localScale;
            //newObject.transform.parent = wallParent.transform;
            wall.gameObject.SetActive(false);
            Instantiate(newObject);
        }

        //Next, figure out the rail order
        GameObject rail = GameObject.Find("Rail");
        List<Transform> list = new List<Transform>();
        foreach(Transform block in rail.transform)
        {
            int n;
            if (int.TryParse(block.gameObject.name, out n))
                list.Add(block);
        }
        list = list.OrderBy(o => int.Parse(o.gameObject.name)).ToList();

        walls = new Queue<Transform>(list);

        if(startAtWall > -1){
            while(walls.Peek().gameObject.name != startAtWall.ToString()){
                walls.Dequeue();
            }
            NavMeshAgent n = GetComponent<NavMeshAgent>();
            n.enabled = false;
            transform.position = GameObject.Find(startAtWall.ToString()).transform.position;
            n.enabled = true;
        }

        agent = GetComponent<NavMeshAgent>();
        target = walls.Dequeue();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("going to block " + target.gameObject.name);
        agent.SetDestination(target.position);
    }

    void OnTriggerEnter(Collider col)
    {
        if (/*col.gameObject.tag == "destination" && */col.gameObject.name == target.gameObject.name)
        {
            target = walls.Dequeue();
            //Destroy(col.gameObject);
        }
    }

    public void setTarget()
    {
        target = walls.Dequeue();
    }
}
