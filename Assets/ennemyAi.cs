using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public EnemyProgressionData progressionData; // Référence vers le ScriptableObject
    public Transform[] waypoints;
    public float waypointTolerance;
    public Transform player;
    public int itemsFound; // Nombre d'objets trouvés par le joueur

    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private int currentWaypointIndex = 0;
    private bool isChasingPlayer = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        navMeshAgent.speed = progressionData.walkSpeed;

        // Mélanger les waypoints
        ShuffleWaypoints();

        MoveToNextWaypoint();
        SetWalkingAnimation();
    }

    void Update()
    {
        UpdateEnemyAttributes(); // Met à jour les attributs en fonction des objets trouvés

        if (isChasingPlayer)
        {
            ChasePlayer();

            if (!CanSeePlayer())
            {
                isChasingPlayer = false;
                MoveToNextWaypoint();
                SetWalkingAnimation();
            }
        }
        else
        {
            Patrol();

            if (CanSeePlayer())
            {
                isChasingPlayer = true;
                SetRunningAnimation();
            }
        }
    }

    // Méthode pour adapter les caractéristiques de l'ennemi en fonction des objets trouvés
    void UpdateEnemyAttributes()
    {
        // Augmenter la vitesse de l'ennemi et réduire le champ de vision en fonction du nombre d'objets trouvés
        float speedMultiplier = 1f + (itemsFound * 0.1f); // Par exemple, augmente de 10% par objet trouvé
        navMeshAgent.speed = isChasingPlayer ? progressionData.runSpeed * speedMultiplier : progressionData.walkSpeed * speedMultiplier;

        float detectionRangeMultiplier = 1f - (itemsFound * 0.05f); // Par exemple, réduit de 5% par objet trouvé
        progressionData.detectionRange *= Mathf.Clamp(detectionRangeMultiplier, 0.5f, 1f); // Limiter la réduction à 50%
    }

    void Patrol()
    {
        navMeshAgent.speed = progressionData.walkSpeed;

        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < waypointTolerance)
            MoveToNextWaypoint();
    }

    void ChasePlayer()
    {
        navMeshAgent.speed = progressionData.runSpeed;
        navMeshAgent.SetDestination(player.position);
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

    void SetWalkingAnimation()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);
    }

    void SetRunningAnimation()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", true);
    }

    // Fonction pour mélanger les waypoints
    void ShuffleWaypoints()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            int randomIndex = Random.Range(i, waypoints.Length);
            Transform temp = waypoints[i];
            waypoints[i] = waypoints[randomIndex];
            waypoints[randomIndex] = temp;
        }
    }
}
