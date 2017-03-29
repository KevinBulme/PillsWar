using UnityEngine;
using System.Collections;

public class Piege : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemie")
        {

            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
