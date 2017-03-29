using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class EnemieIA : MonoBehaviour
{
    public enum EnemieIAType
    {
        Idle = 0, Search, Attack
    }

    protected Transform target;
    protected EnemieIAType aiState = EnemieIAType.Idle;
    protected Vector3 distanceFromPlayer;
    protected bool targetIsDead = false;
    protected bool targetIsHere = false;
    protected Acteur actor;

    public float actionTime = 2.5f;
    public float rotationSpeed = 5.0f;
    public float searchSpeed = 1.5f;
    public float distanceToAttack = 15.0f;
    public float maxYOffset = 3.0f;

    private bool _canShoot = true;
    protected Collider mCollider;
    protected UnityEngine.AI.NavMeshAgent _agent;
    protected Quaternion targetRotation;
    protected Vector3[] destinations;
    protected int positionIndex = 0;
    public float intervalDeTir = 1.0f;

    public float walkRadius = 60.0f;
    public int scoreToWalk = 6;
    public float changeInterval = 0.0f;

    void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            target = player.GetComponent<Transform>();
        else
            throw new UnityException("No Player found !");

        actor = GetComponent<Acteur>();
        aiState = EnemieIAType.Search;

        _agent = GetComponent(typeof(UnityEngine.AI.NavMeshAgent)) as UnityEngine.AI.NavMeshAgent;
        mCollider = GetComponent(typeof(Collider)) as Collider;

        GetRandomDestination();
    }

    void Update()
    {
        if (!targetIsDead)
        {
            distanceFromPlayer = target.position - transform.position;
            targetIsHere = (distanceFromPlayer.magnitude < distanceToAttack && distanceFromPlayer.y < maxYOffset);

            if (aiState != EnemieIAType.Attack && targetIsHere)
            {
                aiState = EnemieIAType.Attack;
                _agent.Stop();
                _agent.velocity = Vector3.zero;
            }

            if (aiState == EnemieIAType.Attack)
            {
                if (!targetIsHere)
                {
                    aiState = EnemieIAType.Search;
                    GetRandomDestination();
                }
                else
                {
                    targetRotation = Quaternion.LookRotation(target.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
                    if (_canShoot)
                    {
                        _canShoot = false;
                        actor.Shoot();
                        Invoke("AllowShoot", intervalDeTir);
                    }
                }
            }

            else if (aiState == EnemieIAType.Search && !_agent.hasPath)
                GetRandomDestination();
        }
    }

    public void AllowShoot()
    {
        _canShoot = true;
    }

    protected virtual void GetRandomDestination()
    {
        var dice0 = Random.Range(0, 6);
        var dice1 = Random.Range(0, 6);

        if (dice0 + dice1 >= scoreToWalk)
        {
            var randomDirection = Random.insideUnitSphere * walkRadius;
            randomDirection += transform.position;

            UnityEngine.AI.NavMeshHit hit;
            UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);

            _agent.SetDestination(hit.position);
        }
    }
}