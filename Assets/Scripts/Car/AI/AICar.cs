using UnityEngine;
using UnityEngine.AI;

public class AICar : MonoBehaviour
{
    public enum CarStates
    {
        Patrol,
        Chase
    }

    public CarStates state;

    [Header("AI Settings")]
    [SerializeField] float patrolRadius = 20f;
    [SerializeField] float chaseDistance = 30f;

    [Header("Gizmos")]
    [SerializeField] Color patrolRadiusColor = Color.red;
    [SerializeField] Color chaseDistanceColor = Color.blue;

    private Transform player;
    private NavMeshAgent navAgent;
    private Animator anim;

    const string ANIMATOR_SIREN_ON = "SirenOn";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        switch(state)
        {
            case CarStates.Patrol:
                Patrol();
                break;
            case CarStates.Chase:
                Chase();
                break;
        }

        //player is in chase radius
        if(Vector3.Distance(transform.position, player.position) < chaseDistance
        && state != CarStates.Chase)
        {
            ChangeState(CarStates.Chase);
            anim.SetBool(ANIMATOR_SIREN_ON, true);
        }
        else if(Vector3.Distance(transform.position, player.position) > chaseDistance)
        {
            ChangeState(CarStates.Patrol);
            anim.SetBool(ANIMATOR_SIREN_ON, false);
        }
        
    }

    void Patrol()
    {
        // Check if the navAgent has no path or it has reached its destination 
        if (!navAgent.hasPath || navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            // Generate a random point within the patrolRadius
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
        
            // Ensure the target position is on the NavMesh
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
            {
                navAgent.SetDestination(hit.position);
            }
        }
    }

    void Chase()
    {
        navAgent.SetDestination(player.position);
    }

    void ChangeState(CarStates newState)
    {
        state = newState;
    }

    void OnDrawGizmosSelected()
    {
        // Draw the patrol radius
        Gizmos.color = patrolRadiusColor;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);

        // Draw the chase distance
        Gizmos.color = chaseDistanceColor;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        // Draw the generated destination point (if it's within the NavMesh)
        if (state == CarStates.Patrol)
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRadius;
            randomDirection += transform.position;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRadius, NavMesh.AllAreas))
            {
                Gizmos.color = patrolRadiusColor;
                Gizmos.DrawSphere(hit.position, 1f);
            }
        }
    }
}
