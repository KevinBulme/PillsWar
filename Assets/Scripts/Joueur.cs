using UnityEngine;
using UnityEngine.UI;

public class Joueur : Acteur
{
    private Rigidbody _rigibody;
    private Vector3 _translation;
    private float _rotation;
    private Vector3 _startPosition;
    private Vector3 _startRotation;
    private int _score = 0;
    public int _munition;

    [Header("Move/Rotate")]
    public float vitesseDeplacement = 10.0f;
    public float vitesseRotation = 65.0f;

    [Header("UI")]
    public Text scoreText;
    public Text vieText;
    public Text HPText;
    public Text munitionText;
    public Text MessageFin;
    public Text Menu;

    [Header("Équipement")]
    public GameObject piege;
    public int nombreDePieges;

    public int NombreEnemies { get; set; }

    public int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreText.text = _score + " point" + ((_score > 1) ? "s" : "");
        }
    }

    public int Munition
    {
        get { return _munition; }
        set
        {
            _munition = value;
            munitionText.text = _munition + " ammo" + ((_munition > 1) ? "s" : "");
        }
    }

    public int Vie { get; set; }

    public int HP
    {

        get { return pointsDeVie; }

        set
        {
            pointsDeVie = value;
            HPText.text = pointsDeVie + " HP";
        }


    }

    void Awake()
    {
        Score = 0;
        Vie = 3;
        Munition = _munition;
    }

    protected override void Start()
    {
        base.Start();

        _rigibody = (Rigidbody)GetComponent(typeof(Rigidbody));

        if (_rigibody == null)
            _rigibody = (Rigidbody)gameObject.AddComponent(typeof(Rigidbody));

        _startPosition = transform.position;
        _startRotation = transform.rotation.eulerAngles;

        NombreEnemies = GameObject.FindObjectsOfType<Enemie>().Length;

        Menu.transform.parent.gameObject.SetActive(true);
    }

    void ToggleMenu()
    {
        if(Menu.transform.parent.gameObject.activeInHierarchy)
        {
            Menu.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            Menu.transform.parent.gameObject.SetActive(true);
        }
    }

    void Update()
    {

        if (HP > 100)
        {
            HP = 100;
        }

        if (NombreEnemies > 0)
        {
            if(!(Menu.transform.parent.gameObject.activeInHierarchy))
            {
                _rotation = Input.GetAxis("Mouse X") * vitesseRotation;

                if (Input.GetKey(KeyCode.A))
                    _rotation = -vitesseRotation;
                else if (Input.GetKey(KeyCode.E))
                    _rotation = vitesseRotation;

                transform.Rotate(0, _rotation, 0);

                _translation.Set(Input.GetAxis("Horizontal") * vitesseDeplacement * Time.deltaTime, 0, Input.GetAxis("Vertical") * vitesseDeplacement * Time.deltaTime);

                transform.Translate(_translation);


                //INPUTS
                if (Input.GetButtonDown("Fire1"))
                {
                    if (Munition > 0)
                    {
                        Shoot();
                        Munition--;
                    
                    }
                }

                //INPUTS
                if (Input.GetButtonDown("Fire2"))
                {
                    if (Munition >= 5)
                    {
                        BigShoot();
                        Munition -= 5;

                    }
                }

                //INPUTS
                if (Input.GetButtonDown("Cancel"))
                {
                    ToggleMenu();
                }

                if (Input.GetKeyDown(KeyCode.X)) //Not implemented yet
                {
                    if (nombreDePieges > 0)
                    {
                        Instantiate(piege, transform.position + new Vector3(0,-1,0),transform.rotation);
                        nombreDePieges--;

                    }
                }
            }
            else
            {
                //INPUTS
                if (Input.GetButtonDown("Cancel"))
                {
                    ToggleMenu();
                }
            }
        }
        else if (NombreEnemies == 0)
        {
            NombreEnemies = -1;
            MessageFin.text = "You won !";
            MessageFin.transform.parent.gameObject.SetActive(true);
        }
    }

    public override void SetDamage(int damages)
    {
        base.SetDamage(damages);
        HPText.text = pointsDeVie + "HP";
    }

    public override void Die()
    {
        if (Vie < 0)
            return;

        Vie--;

        if (Vie <= 0)
        {
            MessageFin.text = "You lost ='(";
            MessageFin.transform.parent.gameObject.SetActive(true);
            NombreEnemies = -1;
            return;
        }

        vieText.text = Vie + " life" + ((Vie > 1) ? "s" : "");

        transform.position = _startPosition;
        transform.rotation = Quaternion.Euler(_startRotation);
        pointsDeVie = 100;
        Munition = 15;
    }
}
