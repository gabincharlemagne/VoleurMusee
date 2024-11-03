using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public List<string> itemsToFind = new List<string>(); // Liste des objets à trouver
    public TextMeshProUGUI itemListText; // Référence au TextMeshPro pour l'affichage
    public GameObject victoryPanel; // Référence au panneau de victoire

    void Start()
    {
        // Remplissez la liste d'objets à trouver ici
        itemsToFind.Add("Cube");

        // Cache le panneau de victoire au démarrage
        victoryPanel.SetActive(false);

        // Met à jour l'UI au démarrage
        UpdateItemListUI();
    }

    public void FindItem(string itemName)
    {
        if (itemsToFind.Contains(itemName))
        {
            itemsToFind.Remove(itemName); // Supprime l'objet trouvé de la liste
            UpdateItemListUI(); // Met à jour l'UI après avoir trouvé un objet

            // Vérifie si tous les objets ont été trouvés
            if (itemsToFind.Count == 0)
            {
                DisplayVictoryScreen();
            }
        }
    }

    void UpdateItemListUI()
    {
        itemListText.text = "Find a : \n"; // Réinitialise le texte
        foreach (string item in itemsToFind)
        {
            itemListText.text += "- " + item + "\n"; // Ajoute chaque objet à la liste affichée
        }
    }

    void DisplayVictoryScreen()
    {
        victoryPanel.SetActive(true); // Affiche le panneau de victoire
        Time.timeScale = 0; // Arrête le jeu si souhaité
    }
}
