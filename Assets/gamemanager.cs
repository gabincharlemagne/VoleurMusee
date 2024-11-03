using UnityEngine;
using UnityEngine.UI;
using TMPro; // Assure-toi d'inclure ce namespace si tu utilises TextMeshPro

public class Timer : MonoBehaviour
{
    public float totalTime = 600f;
    public TMP_Text timerText; // Remplace Text par TMP_Text si tu utilises TextMeshPro
    public GameObject gameOverPanel;

    private float remainingTime;
    private bool gameEnded = false;

    void Start()
    {
        remainingTime = totalTime;
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (gameEnded) return;

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

    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverPanel.SetActive(true);
    }
}
