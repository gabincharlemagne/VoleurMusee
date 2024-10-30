using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public List<string> itemsToFind = new List<string>(); // Liste des objets à trouver
    public TextMeshProUGUI itemListText; // Référence au TextMeshPro pour l'affichage

    void Start()
    {
        // Remplissez la liste d'objets à trouver ici
        itemsToFind.Add("Cube");


        // Mettez à jour l'UI au démarrage
        UpdateItemListUI();
    }

    public void FindItem(string itemName)
    {
        if (itemsToFind.Contains(itemName))
        {
            itemsToFind.Remove(itemName); // Supprime l'objet trouvé de la liste
            UpdateItemListUI(); // Met à jour l'UI après avoir trouvé un objet
        }
    }

    void UpdateItemListUI()
    {
        itemListText.text = "Objets à trouver:\n"; // Réinitialise le texte
        foreach (string item in itemsToFind)
        {
            itemListText.text += "- " + item + "\n"; // Ajoute chaque objet à la liste affichée
        }
    }
}
