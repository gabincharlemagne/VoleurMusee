using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Transform enemy; // Référence à l'ennemi
    public float deathDistance = 1f; // Distance à laquelle le joueur meurt
    public GameObject gameOverPanel; // Référence au panneau Game Over
    public MonoBehaviour cameraController; // Référence au script de la caméra

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

        // Assure-toi que le curseur est déverrouillé au départ
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Calculer la distance entre le joueur et l'ennemi
        float distance = Vector3.Distance(transform.position, enemy.position);

        // Si la distance est inférieure à la distance de mort, appeler la méthode Die()
        if (distance < deathDistance)
        {
            Die();
        }

        // Supprime la vérification pour la touche "Entrée" si on ne veut plus de raccourci clavier
        //if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        //{
        //    Respawn();
        //}
    }

    // Méthode pour gérer la mort du joueur
    void Die()
    {
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

        // Déverrouiller et afficher le curseur pour cliquer sur le bouton
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0; // Met le jeu en pause
    }

    // Méthode pour respawn le joueur
    public void Respawn()
    {
        // Reprendre le jeu en remettant le TimeScale à 1
        Time.timeScale = 1;

        // Recharger la scène actuelle pour redémarrer le jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // Réactiver le mouvement de la caméra
        if (cameraController != null)
        {
            cameraController.enabled = true;
        }

        // Verrouiller et masquer le curseur après le respawn
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
