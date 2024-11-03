using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Pour g�rer le Game Over

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 6; // 10 minutes en secondes
    public Text timerText; // R�f�rence � un Text UI pour afficher le temps
       

    private bool timerIsRunning = true;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60F);
            int seconds = Mathf.FloorToInt(timeRemaining % 60F);
            timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00"); // Affiche le temps

            // V�rifie si le temps est �coul�
            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }


    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void GameOver()
    {
        // Affiche l��cran de Game Over ou recharge la sc�ne
        SceneManager.LoadScene("GameOverScene"); // Cr�e une sc�ne de game over ou g�re autrement
    }
}
