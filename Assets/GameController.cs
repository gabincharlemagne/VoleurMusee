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
    public MonoBehaviour cameraController;

    [Header("Game Elements")]
    public GameObject[] uiElementsToHide;

    [Header("Timer Settings")]
    public float totalTime = 600f;
    public TMP_Text timerText;

    private bool isPaused = false;
    private bool gameEnded = false;
    private float remainingTime;
    
    public BestTimesData bestTimesData;
    public TMP_Text bestTimesText;
    public TMP_Text currentTimeText;

    private void Start()
    {
        remainingTime = totalTime;
        gameOverPanel.SetActive(false);
        victoryPanel.SetActive(false);
        pauseMenuPanel.SetActive(false);
    }

    private void Update()
    {
        // Bloque la possibilité de pause si le panneau de victoire ou de game over est actif
        if (!gameEnded && !victoryPanel.activeSelf && !gameOverPanel.activeSelf)
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

            UpdateTimer();
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
            cameraController.enabled = false;
        }

        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
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
            cameraController.enabled = false;
        }

        float currentTime = totalTime - remainingTime;
        DisplayCurrentTime(currentTime);
        bestTimesData.AddTime(currentTime);
        UpdateBestTimeUI();

        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }

    private void DisplayCurrentTime(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        currentTimeText.text = $"Your Time: {minutes:00}:{seconds:00}";
    }

    private void UpdateBestTimeUI()
    {
        float bestTime = bestTimesData.GetBestTime();

        if (bestTime != float.MaxValue)
        {
            int minutes = Mathf.FloorToInt(bestTime / 60);
            int seconds = Mathf.FloorToInt(bestTime % 60);
            bestTimesText.text = $"Best Time: {minutes:00}:{seconds:00}";
        }
        else
        {
            bestTimesText.text = "Best Time: --:--";
        }
    }

    public void PauseGame()
    {
        if (gameEnded || victoryPanel.activeSelf || gameOverPanel.activeSelf) return; // Empêche la pause si le jeu est terminé ou si un panneau est actif

        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isPaused = true;

        if (cameraController != null)
        {
            cameraController.enabled = false;
        }

        foreach (GameObject uiElement in uiElementsToHide)
        {
            uiElement.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        if (gameEnded || victoryPanel.activeSelf || gameOverPanel.activeSelf) return; // Empêche la reprise si le jeu est terminé ou si un panneau est actif

        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;

        if (cameraController != null)
        {
            cameraController.enabled = true;
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
