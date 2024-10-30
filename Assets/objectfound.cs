using UnityEngine;

public class ObjectFound : MonoBehaviour
{
    public string itemName; // Le nom de l'objet que le joueur doit trouver
    public ItemTracker itemTracker; // R�f�rence au script ItemTracker

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // V�rifie si l'objet qui entre en collision est le joueur
        {
            itemTracker.FindItem(itemName); // Appelle la fonction pour indiquer que l'objet est trouv�
            Destroy(gameObject); // D�truit l'objet dans la sc�ne
        }
    }
}
