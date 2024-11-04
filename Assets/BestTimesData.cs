using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BestTimesData", menuName = "Game/BestTimesData")]
public class BestTimesData : ScriptableObject
{
    public List<float> bestTimes = new List<float>();

    public void AddTime(float newTime)
    {
        bestTimes.Add(newTime);
        bestTimes.Sort(); // Trie les temps dans l'ordre croissant
        if (bestTimes.Count > 5) // Garde seulement les 5 meilleurs temps
        {
            bestTimes.RemoveAt(bestTimes.Count - 1);
        }
    }

    public float GetBestTime()
    {
        if (bestTimes.Count > 0)
        {
            return bestTimes[0]; // Retourne le meilleur temps (le plus rapide)
        }
        else
        {
            return float.MaxValue; // Retourne une valeur élevée si aucun score n'est enregistré
        }
    }
}