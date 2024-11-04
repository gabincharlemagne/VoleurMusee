using UnityEngine;

public class EnemyFootsteps : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip[] walkFootstepClips; // Sons de marche
    public AudioClip[] runFootstepClips; // Sons de course

    public float walkFootstepInterval = 0.5f; // Intervalle entre les sons de pas en marchant
    public float runFootstepInterval = 0.3f; // Intervalle entre les sons de pas en courant

    private Animator animator;
    private float footstepTimer = 0f;
    private bool isWalking = false;
    private bool isRunning = false;

    private void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator non trouvé sur l'ennemi.");
        }
    }

    private void Update()
    {
        // Mettez à jour les états depuis l'Animator
        isWalking = animator.GetBool("isWalking");
        isRunning = animator.GetBool("isRunning");


        // Si l'ennemi est en mouvement (marche ou course)
        if (isWalking || isRunning)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                if (isRunning)
                {
                    PlayFootstep(runFootstepClips);
                    footstepTimer = runFootstepInterval;
                }
                else if (isWalking)
                {
                    PlayFootstep(walkFootstepClips);
                    footstepTimer = walkFootstepInterval;
                }
            }
        }
    }

    private void PlayFootstep(AudioClip[] clips)
    {
        if (clips.Length > 0)
        {
            // Sélectionne un son de pas aléatoire dans le tableau
            AudioClip clip = clips[Random.Range(0, clips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
