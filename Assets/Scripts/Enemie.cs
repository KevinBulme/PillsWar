using UnityEngine;

public class Enemie : Acteur 
{
	public int NbPoints = 10;

	public GameObject objetMort;

    public override void Die()
	{
        var go = GameObject.FindWithTag("Player");
        var player = (Joueur)go.GetComponent(typeof(Joueur));
        player.Score += NbPoints;
        player.NombreEnemies--;
		if(Random.Range(0,40)<NbPoints){
		Instantiate(objetMort,transform.position + new Vector3(0,-1,0),transform.rotation);
		}
        Destroy (gameObject);
	}  
}
