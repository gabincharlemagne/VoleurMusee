using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public List<string> itemsToFind = new List<string>();
    public TextMeshProUGUI itemListText;

    private GameController gameController;

    private void Start()
    {
        // Trouver le GameController dans la scène
        gameController = FindObjectOfType<GameController>();

        // Vérifie que le GameController a bien été trouvé
        if (gameController == null)
        {
            Debug.LogError("GameController non trouvé dans la scène. Assurez-vous qu'il est présent dans la hiérarchie.");
        }

        // Ajouter les objets à trouver
        itemsToFind.Add("Cube");
        itemsToFind.Add("Lion");
        itemsToFind.Add("Greek status");
        itemsToFind.Add("Einstein");

        // Met à jour l'UI au démarrage
        UpdateItemListUI();
    }


    public void FindItem(string itemName)
    {
        if (itemsToFind.Contains(itemName))
        {
            itemsToFind.Remove(itemName);
            UpdateItemListUI();

            if (itemsToFind.Count == 0)
            {
                gameController.ShowVictoryScreen(); // Appelle la fonction de GameController
            }
        }
    }

    private void UpdateItemListUI()
    {
        itemListText.text = "Find a : \n";
        foreach (string item in itemsToFind)
        {
            itemListText.text += "- " + item + "\n";
        }
    }
}