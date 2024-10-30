using UnityEngine;

public class ObjectFound : MonoBehaviour
{
    public string itemName; // Le nom de l'objet que le joueur doit trouver
    public ItemTracker itemTracker; // Référence au script ItemTracker

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Vérifie si l'objet qui entre en collision est le joueur
        {
            itemTracker.FindItem(itemName); // Appelle la fonction pour indiquer que l'objet est trouvé
            Destroy(gameObject); // Détruit l'objet dans la scène
        }
    }
}
