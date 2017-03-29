using UnityEngine;
using System.Collections;

public class PackVie : MonoBehaviour {

    public int nombreDeVie;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            if (col.GetComponent<Joueur>().HP < 100)
            {
                col.GetComponent<Joueur>().HP += nombreDeVie;
                Destroy(gameObject);
            }
        }
    }
}
