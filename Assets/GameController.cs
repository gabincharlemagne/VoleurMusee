using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject mainMenuPanel; // Panel du menu principal
    public GameObject gameUIPanel;   // Panel de l'UI du jeu (radar et liste d'objets)

    private void Start()
    {
        ShowMainMenu(); // Affiche le menu principal au démarrage
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false); // Cache le menu principal
        gameUIPanel.SetActive(true);    // Affiche l'interface de jeu (radar et liste d'objets)
        
        // Verrouille le curseur et le rend invisible pour le gameplay
        // Verrouille le curseur et le rend invisible si nécessaire pour le gameplay
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Démarre ici d'autres éléments du gameplay si nécessaire
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true); // Affiche le menu principal
        gameUIPanel.SetActive(false);  // Cache l'interface de jeu
        
        // Libère le curseur et le rend visible dans le menu principal
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void QuitGame()
    {
        // Quitte le jeu (ne fonctionne que dans la version buildée)
        Application.Quit();
    }
}