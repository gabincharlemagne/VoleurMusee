using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BestTimesData", menuName = "Game/BestTimesData")]
public class BestTimesData : ScriptableObject
{
    public List<float> bestTimes = new List<float>();

    public void AddTime(float newTime)
    {
        bestTimes.Add(newTime);
        bestTimes.Sort(); // Tri les temps dans l'ordre croissant (meilleurs temps en premier)
        if (bestTimes.Count > 5) // Par exemple, on garde seulement les 5 meilleurs temps
        {
            bestTimes.RemoveAt(bestTimes.Count - 1); // Supprime le temps le plus lent
        }
    }
}