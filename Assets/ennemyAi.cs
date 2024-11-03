using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyProgressionData progressionData; // Référence vers le ScriptableObject
    public Transform[] waypoints;
    public float waypointTolerance;
    public Transform player;

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private bool isChasingPlayer = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        navMeshAgent.speed = progressionData.walkSpeed; // Utiliser la vitesse de marche du ScriptableObject
        MoveToNextWaypoint();
    }

    void Update()
    {
        navMeshAgent.speed = isChasingPlayer ? progressionData.runSpeed : progressionData.walkSpeed;

        if (isChasingPlayer)
        {
            navMeshAgent.SetDestination(player.position);
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);

            if (!CanSeePlayer())
            {
                isChasingPlayer = false;
                MoveToNextWaypoint();
            }
        }
        else
        {
            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < waypointTolerance)
                MoveToNextWaypoint();

            if (CanSeePlayer())
                isChasingPlayer = true;
        }
    }

    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude < progressionData.detectionRange && angleToPlayer < progressionData.fieldOfViewAngle)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out RaycastHit hit, progressionData.detectionRange))
            {
                if (hit.transform == player)
                    return true;
            }
        }

        return false;
    }
}
