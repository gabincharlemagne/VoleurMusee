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
        itemsToFind.Add("Cube");
<<<<<<< HEAD
<<<<<<< HEAD
        gameController = FindObjectOfType<GameController>();
=======
        itemsToFind.Add("Lion");
        itemsToFind.Add("Greek status");
        itemsToFind.Add("Einstein");
=======
        itemsToFind.Add("Lion");
        itemsToFind.Add("Greek status");
        itemsToFind.Add("Einstein");


>>>>>>> c5888254ac75f3f1da262d13c802d745f71ae5c8


>>>>>>> c5888254ac75f3f1da262d13c802d745f71ae5c8

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