using UnityEngine;

public class CameraJoueur : MonoBehaviour 
{
	private Quaternion _rotation = Quaternion.identity;
	public Transform joueur;
	public Vector3 decalage = Vector3.zero;
	public float vitesseRotation = 5;
	
	void Start () 
	{
		if (joueur == null)
			joueur = GameObject.FindWithTag("Player").transform;
		
		if (decalage == Vector3.zero)
			decalage = joueur.position - transform.position;	
	}
	
	void LateUpdate() 
	{		
		_rotation = Quaternion.Euler(0.0f, joueur.eulerAngles.y, 0.0f);
		transform.position = joueur.position - (_rotation * decalage);
		transform.LookAt(joueur.position);
	}
}
