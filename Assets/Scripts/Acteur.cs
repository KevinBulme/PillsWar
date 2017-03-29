using UnityEngine;

public abstract class Acteur : MonoBehaviour
{
    [Header("Point de vie")]
    public int pointsDeVie = 100;

    [Header("Réglage du tir")]
    public Transform pointDeTir;
    public GameObject prefabMunition;
    public GameObject prefabBadassMunition;

    protected virtual void Start()
    {
        if (prefabMunition == null)
            prefabMunition = (GameObject)Resources.Load("MunitionDeBase", typeof(GameObject));

        if (prefabBadassMunition == null)
            prefabBadassMunition = (GameObject)Resources.Load("munition_badass", typeof(GameObject));

        if (pointDeTir == null)
            pointDeTir = transform.GetChild(0);
    }

    public void Shoot()
    {
        var ammo = (GameObject)Instantiate(prefabMunition, pointDeTir.position, Quaternion.identity);
        var rb = (Rigidbody)ammo.GetComponent(typeof(Rigidbody));
        var ammoScript = (Munition)ammo.GetComponent(typeof(Munition));
        ammoScript.emmeteur = name;
        rb.AddForce(pointDeTir.forward * ammoScript.puissance);
    }

    public void BigShoot()
    {
        var ammo = (GameObject)Instantiate(prefabBadassMunition, pointDeTir.position, Quaternion.identity);
        var rb = (Rigidbody)ammo.GetComponent(typeof(Rigidbody));
        var ammoScript = (Munition)ammo.GetComponent(typeof(Munition));
        ammoScript.emmeteur = name;
        rb.AddForce(pointDeTir.forward * ammoScript.puissance);
    }

    public virtual void SetDamage(int damages)
    {
        pointsDeVie -= damages;
        pointsDeVie = pointsDeVie < 0 ? 0 : pointsDeVie;
        if (pointsDeVie <= 0)
            Die();
    }

    public abstract void Die();
}
