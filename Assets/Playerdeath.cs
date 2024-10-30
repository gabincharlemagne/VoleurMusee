using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    public Transform enemy; // R�f�rence � l'ennemi
    public float deathDistance = 1f; // Distance � laquelle le joueur meurt
    public GameObject gameOverPanel; // R�f�rence au panneau Game Over
    public MonoBehaviour cameraController; // R�f�rence au script de la cam�ra

    void Start()
    {
        // Masquer le Game Over Panel au d�but
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (cameraController != null)
        {
            cameraController.enabled = true; // Activer le contr�le de la cam�ra au d�but
        }

        // Assure-toi que le curseur est d�verrouill� au d�part
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        // Calculer la distance entre le joueur et l'ennemi
        float distance = Vector3.Distance(transform.position, enemy.position);

        // Si la distance est inf�rieure � la distance de mort, appeler la m�thode Die()
        if (distance < deathDistance)
        {
            Die();
        }

        // V�rifier si le joueur appuie sur "Entr�e" pour respawn apr�s �tre mort
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            Respawn();
        }
    }

    // M�thode pour g�rer la mort du joueur
    void Die()
    {
        // Afficher l'�cran Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // D�sactiver le script de la cam�ra pour arr�ter son mouvement
        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        // Verrouiller le curseur pour emp�cher le contr�le de la cam�ra
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 0; // Met le jeu en pause
    }

    // M�thode pour respawn le joueur
    public void Respawn()
    {
        // Reprendre le jeu en remettant le TimeScale � 1
        Time.timeScale = 1;

        // Recharger la sc�ne actuelle pour red�marrer le jeu
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        // D�verrouiller le curseur pour permettre l'utilisation de la souris apr�s le respawn
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
