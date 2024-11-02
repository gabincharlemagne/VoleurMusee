using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracker : MonoBehaviour
{
    public List<string> itemsToFind = new List<string>(); // Liste des objets � trouver
    public TextMeshProUGUI itemListText; // R�f�rence au TextMeshPro pour l'affichage

    void Start()
    {
        // Remplissez la liste d'objets � trouver ici
        itemsToFind.Add("Cube");


        // Mettez � jour l'UI au d�marrage
        UpdateItemListUI();
    }

    public void FindItem(string itemName)
    {
        if (itemsToFind.Contains(itemName))
        {
            itemsToFind.Remove(itemName); // Supprime l'objet trouv� de la liste
            UpdateItemListUI(); // Met � jour l'UI apr�s avoir trouv� un objet
        }
    }

    void UpdateItemListUI()
    {
        itemListText.text = "Find a : \n"; // R�initialise le texte
        foreach (string item in itemsToFind)
        {
            itemListText.text += "- " + item + "\n"; // Ajoute chaque objet � la liste affich�e
        }
    }
}
