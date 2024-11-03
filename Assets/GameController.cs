using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject pauseMenuPanel;
    public GameObject victoryPanel;
    public GameObject gameOverPanel;

    [Header("Camera Controller")]
    public MonoBehaviour cameraController; // Référence au script de contrôle de la caméra

    [Header("Game Elements")]
    public GameObject[] uiElementsToHide; // Éléments d'UI à masquer pendant la pause

    [Header("Timer Settings")]
    public float totalTime = 600f;
    public TMP_Text timerText;

    private bool isPaused = false;
    private bool gameEnded = false;
    private float remainingTime;
    
    public BestTimesData bestTimesData; // Référence au ScriptableObject pour les meilleurs temps
    public TMP_Text bestTimesText; // Référence à l'UI pour afficher les meilleurs temps


    private void Start()
    {
        remainingTime = totalTime;
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (!gameEnded)
        {
            UpdateTimer();

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
    }

    private void UpdateTimer()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            EndGame();
        }
        else
        {
            UpdateTimerText();
        }
    }

    private void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void EndGame()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraController != null)
        {
            cameraController.enabled = false; // Désactive le mouvement de la caméra
        }
    }

    public void ShowVictoryScreen()
    {
        gameEnded = true;
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (cameraController != null)
        {
            cameraController.enabled = false; // Désactive le mouvement de la caméra
        }
        
        // Ajouter le temps actuel aux meilleurs temps
        bestTimesData.AddTime(totalTime - remainingTime);
        UpdateBestTimesUI(); // Affiche les meilleurs temps dans l'UI de victoire
        
        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }
    
    private void UpdateBestTimesUI()
    {
        bestTimesText.text = "Best Times:\n";
        for (int i = 0; i < bestTimesData.bestTimes.Count; i++)
        {
            int minutes = Mathf.FloorToInt(bestTimesData.bestTimes[i] / 60);
            int seconds = Mathf.FloorToInt(bestTimesData.bestTimes[i] % 60);
            bestTimesText.text += string.Format("{0}: {1:00}:{2:00}\n", i + 1, minutes, seconds);
        }
    }

    public void PauseGame()
    {
        if (gameEnded) return; // Empêche la pause si le jeu est terminé

        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;

        if (cameraController != null)
        {
            cameraController.enabled = false; // Désactive le script de contrôle de la caméra
        }

        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        if (gameEnded) return; // Empêche la reprise du jeu si celui-ci est terminé

        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;

        if (cameraController != null)
        {
            cameraController.enabled = true; // Réactive le script de contrôle de la caméra
        }

        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(true);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
