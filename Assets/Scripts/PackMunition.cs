using UnityEngine;
using System.Collections;

public class PackMunition : MonoBehaviour {

    public int nombreDeMunitions;


	void Start () {
	
	}
	
	void Update () {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {

            col.GetComponent<Joueur>().Munition += nombreDeMunitions;
            Destroy(gameObject);
        }
    }

}
