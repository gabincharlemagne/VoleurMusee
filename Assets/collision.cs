using UnityEngine;

public class Player : MonoBehaviour
{
    // R�f�rence au script ItemTracker
    public ItemTracker itemTracker;

    void OnTriggerEnter(Collider other)
    {
        // V�rifie si le joueur entre en collision avec un objet ayant le script ObjectFound
        ObjectFound objectFound = other.GetComponent<ObjectFound>();
        if (objectFound != null)
        {
            // Appelle la fonction FindItem du script ItemTracker
            itemTracker.FindItem(objectFound.itemName);
        }
    }
}
