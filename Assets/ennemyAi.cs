using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform[] waypoints; // Liste des points de passage
    public float waypointTolerance; // Distance tolérée pour atteindre le waypoint
    public float detectionRange; // Distance de détection du joueur
    public float fieldOfViewAngle; // Angle du champ de vision de l'ennemi
    public Transform player; // Référence au joueur

    public float walkSpeed; // Vitesse de marche
    public float runSpeed; // Vitesse de course

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private bool isChasingPlayer = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // Assurez-vous que l'Animator est sur l'enfant si nécessaire
        navMeshAgent.speed = walkSpeed; // Initialiser avec la vitesse de marche
        MoveToNextWaypoint();
    }

    void Update()
    {
        if (isChasingPlayer)
        {
            // Poursuite du joueur
            navMeshAgent.speed = runSpeed;
            navMeshAgent.SetDestination(player.position);

            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);

            if (!CanSeePlayer())
            {
                isChasingPlayer = false;
                MoveToNextWaypoint();
                Debug.Log("Joueur perdu, retour à la patrouille");
            }
        }
        else
        {
            // Patrouille entre les waypoints
            navMeshAgent.speed = walkSpeed;
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);

            if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < waypointTolerance)
            {
                Debug.Log("Waypoint atteint, passage au suivant");
                MoveToNextWaypoint();
            }

            if (CanSeePlayer())
            {
                isChasingPlayer = true;
                Debug.Log("Joueur détecté, début de poursuite");
            }
        }
    }


    void MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        Debug.Log("Nouvelle destination : Waypoint " + currentWaypointIndex);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }


    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude < detectionRange && angleToPlayer < fieldOfViewAngle)
        {
            Ray ray = new Ray(transform.position, directionToPlayer);
            RaycastHit hit;

            // Utiliser un raycast pour s'assurer qu'il n'y a pas d'obstacle entre l'ennemi et le joueur
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.transform == player)
                {
                    return true; // Le joueur est dans le champ de vision
                }
            }
        }

        return false; // Le joueur n'est pas visible
    }
}
