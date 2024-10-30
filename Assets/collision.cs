using UnityEngine;

public class Player : MonoBehaviour
{
    // Référence au script ItemTracker
    public ItemTracker itemTracker;

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si le joueur entre en collision avec un objet ayant le script ObjectFound
        ObjectFound objectFound = other.GetComponent<ObjectFound>();
        if (objectFound != null)
        {
            // Appelle la fonction FindItem du script ItemTracker
            itemTracker.FindItem(objectFound.itemName);
        }
    }
}
