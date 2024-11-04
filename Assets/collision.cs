using UnityEngine;

public class Player : MonoBehaviour
{
    // R�f�rence au script ItemTracker
    public ItemTracker itemTracker;

    void OnTriggerEnter(Collider other)
    {
        // Vérifie si l'objet avec lequel le joueur entre en collision est un objet à trouver
        if (itemTracker != null && other.CompareTag("Collectible"))
        {
            // Appelle la fonction CollectItem de ItemTracker avec l'objet trouvé
            itemTracker.CollectItem(other.gameObject);
        }
    }
}
