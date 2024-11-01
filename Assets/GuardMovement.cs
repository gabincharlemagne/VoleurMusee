using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardMovement : MonoBehaviour
{
    private Animator animator;
    private UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        // Récupère l'Animator et le NavMeshAgent du garde
        animator = GetComponent<Animator>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        // Vérifie si le garde est en train de se déplacer
        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
