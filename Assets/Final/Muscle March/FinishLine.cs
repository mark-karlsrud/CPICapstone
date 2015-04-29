using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        col.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        scaleAndRestart();
    }

    public IEnumerator scaleAndRestart()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);

    }
}
