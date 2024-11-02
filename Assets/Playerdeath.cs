using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Transform enemy; // Référence à l'ennemi
    public float deathDistance = 1f; // Distance à laquelle le joueur meurt
    public GameObject gameOverPanel; // Référence au panneau Game Over
    public MonoBehaviour cameraController; // Référence au script de la caméra
    private bool isDead = false;

    void Start()
    {
        // Masquer le Game Over Panel au début
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (cameraController != null)
        {
            cameraController.enabled = true; // Activer le contrôle de la caméra au début
        }

        // Verrouiller et masquer le curseur au départ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // S'abonner à l'événement de chargement de la scène
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Update()
    {
        if (isDead) return;

        // Calculer la distance entre le joueur et l'ennemi
        float distance = Vector3.Distance(transform.position, enemy.position);

        // Si la distance est inférieure à la distance de mort, appeler la méthode Die()
        if (distance < deathDistance)
        {
            Die();
        }

        // Vérifier si le joueur appuie sur "Entrée" pour respawn après être mort
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            Respawn();
        }
    }

    // Méthode pour gérer la mort du joueur
    void Die()
    {
        isDead = true;

        // Afficher l'écran Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Désactiver le script de la caméra pour arrêter son mouvement
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Déverrouiller et afficher le curseur pour permettre l'interaction avec le panneau Game Over
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Mettre le jeu en pause
        Time.timeScale = 0;
    }

    // Méthode pour respawn le joueur
    public void Respawn()
    {
        // Reprendre le jeu en remettant Time.timeScale à 1
        Time.timeScale = 1;

        // Déverrouiller le curseur pour permettre l'utilisation de la souris après le respawn
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Recharger la scène actuelle pour redémarrer le jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Méthode appelée après le chargement de la scène
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Réinitialiser les paramètres de mort et réactiver le mouvement
        isDead = false;

        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Se désabonner de l'événement pour éviter de multiples appels
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Assurez-vous de se désabonner de l'événement pour éviter les erreurs de référence
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
