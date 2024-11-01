using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoTorchInRadar : MonoBehaviour
{
    public Camera radarCamera; // Assigne la caméra radar dans l'inspecteur

    void Start()
    {
        if (radarCamera != null)
        {
            // Désactiver la torche pour la caméra radar
            GetComponent<Renderer>().enabled = false;
        }
    }

    void Update()
    {
        // S'assurer que la torche reste invisible sur la caméra radar
        if (radarCamera != null && radarCamera.enabled)
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
