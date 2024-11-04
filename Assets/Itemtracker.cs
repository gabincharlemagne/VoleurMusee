using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTracker : MonoBehaviour
{
    [Header("UI Elements")]
    public Image[] itemImages; // Tableau des 4 images dans le panel

    [Header("Item Settings")]
    public Sprite[] itemSprites; // Tableau de sprites pour les objets disponibles
    public int numberOfItemsToFind = 4; // Nombre d'objets à trouver

    [Header("Game Controller Reference")]
    public GameController gameController; // Référence au GameController pour appeler ShowVictoryScreen

    private List<GameObject> allCollectibles; // Liste de tous les objets collectables dans la scène
    private List<GameObject> itemsToFind; // Liste des objets choisis pour être trouvés
    private Dictionary<GameObject, Image> collectibleImageMap; // Dictionnaire pour mapper objets et images

    void Start()
    {
        // Récupérer tous les objets avec le tag "Collectible"
        allCollectibles = new List<GameObject>(GameObject.FindGameObjectsWithTag("Collectible"));
        itemsToFind = new List<GameObject>();
        collectibleImageMap = new Dictionary<GameObject, Image>();

        // Sélectionner aléatoirement des objets à partir de la liste de tous les objets collectables
        for (int i = 0; i < numberOfItemsToFind; i++)
        {
            if (allCollectibles.Count == 0) break;

            int randomIndex = Random.Range(0, allCollectibles.Count);
            GameObject selectedObject = allCollectibles[randomIndex];
            itemsToFind.Add(selectedObject);
            allCollectibles.RemoveAt(randomIndex);

            // Assigner l'image correspondante au slot
            Sprite collectibleSprite = GetSpriteForCollectible(selectedObject.name);
            if (collectibleSprite != null && i < itemImages.Length)
            {
                itemImages[i].sprite = collectibleSprite;
                itemImages[i].enabled = true; // Activer l'image
                collectibleImageMap[selectedObject] = itemImages[i]; // Associer l'objet à l'image dans le dictionnaire
            }
        }
    }

    Sprite GetSpriteForCollectible(string itemName)
    {
        // Trouver le sprite correspondant à l'objet en fonction de son nom
        foreach (Sprite sprite in itemSprites)
        {
            if (sprite.name == itemName) // Assurez-vous que le nom du sprite correspond au nom de l'objet
            {
                return sprite;
            }
        }
        return null; // Retourne null si aucun sprite correspondant n'est trouvé
    }

    public void CollectItem(GameObject item)
    {
        if (itemsToFind.Contains(item))
        {
            // Retirer l'objet de la liste des objets à trouver
            itemsToFind.Remove(item);

            // Désactiver l'image associée dans le dictionnaire
            if (collectibleImageMap.ContainsKey(item))
            {
                Image image = collectibleImageMap[item];
                image.enabled = false;
                collectibleImageMap.Remove(item); // Retirer l'objet du dictionnaire
            }

            Destroy(item); // Supprimer l'objet dans la scène

            // Afficher l'écran de victoire si tous les objets ont été trouvés
            if (itemsToFind.Count == 0)
            {
                DisplayVictoryScreen();
            }
        }
    }

    void DisplayVictoryScreen()
    {
        // Appeler la fonction ShowVictoryScreen() dans GameController pour afficher le panneau de victoire
        if (gameController != null)
        {
            gameController.ShowVictoryScreen();
        }
        else
        {
            Debug.LogWarning("GameController n'est pas assigné dans ItemTracker.");
        }
    }
}
