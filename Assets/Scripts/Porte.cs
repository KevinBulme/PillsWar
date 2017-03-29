using UnityEngine;
using System.Collections;

public class Porte : MonoBehaviour {

    public bool inTrigger;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	   if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Destroy(gameObject);
            }
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            inTrigger = false;
        }
    }
}
