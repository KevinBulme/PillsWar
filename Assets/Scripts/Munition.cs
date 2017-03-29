using UnityEngine;

public class Munition : MonoBehaviour 
{
    public int pointDeDegat = 25;
	public float puissance = 200;
    public float tempsDeVie = 3.5f;

    [HideInInspector]
	public string emmeteur = "";

    void Start()
    {
        Invoke("Die", tempsDeVie);
    }

	void OnCollisionEnter(Collision other)
	{
        HandleCollision(other.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
        HandleCollision(other.gameObject);
	}

	private void HandleCollision(GameObject go)
	{
		if (go.name == emmeteur || go.name == name)
			return;

        if (go.tag == "ammo")
            return;

        var actor = go.GetComponent<Acteur>();
        if (actor != null)
            actor.SetDamage(pointDeDegat);

        Die();
	}

    private void Die()
    {
        Destroy(gameObject);
    }
}
