using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Le panel de l'UI pour le menu de pause
    public MonoBehaviour cameraController; // Référence au script de contrôle de la caméra
    public GameObject[] uiElementsToHide; // Tableau pour les éléments d'UI à cacher
    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // Affiche le menu de pause
        Time.timeScale = 0f; // Met le jeu en pause
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;

        if (cameraController != null)
        {
            cameraController.enabled = false; // Désactive le script de contrôle de la caméra
        }

        // Désactive tous les éléments du tableau
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Cache le menu de pause
        Time.timeScale = 1f; // Reprend le jeu
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;

        if (cameraController != null)
        {
            cameraController.enabled = true; // Réactive le script de contrôle de la caméra
        }

        // Réactive tous les éléments du tableau
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(true);
        }
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Remet le jeu à la vitesse normale avant de changer de scène
        SceneManager.LoadScene("MainMenu"); // Assure-toi que le nom de la scène du menu principal est correct
    }
}
