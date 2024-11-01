using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarCameraFollow : MonoBehaviour
{
    public Transform target; // Cible à suivre
    public float height = 10.0f; // Hauteur au-dessus de la cible

    void LateUpdate()
    {
        if (target != null)
        {
            // Met à jour la position de la caméra pour qu'elle soit directement au-dessus de la cible à la hauteur spécifiée
            transform.position = new Vector3(target.position.x, target.position.y + height, target.position.z);
            // Assure que la caméra pointe toujours vers le bas
            transform.rotation = Quaternion.Euler(90.0f, 0, 0);
        }
    }
}
