using UnityEngine;
using UnityEngine.UI;

public class RadarIndicator : MonoBehaviour
{
    public Transform player; // Position du joueur
    public Transform enemy; // Position de l'IA à suivre
    public RectTransform radarUI; // Le UI du radar (Raw Image du radar)
    public Image enemyIndicator; // L'image rouge représentant l'IA sur le radar
    public float radarRange = 50f; // Distance maximale pour que l'IA apparaisse sur le radar

    private void Update()
    {
        // Calcul de la distance entre le joueur et l'IA
        float distance = Vector3.Distance(player.position, enemy.position);

        if (distance <= radarRange)
        {
            // Calcul de la position relative de l'IA par rapport au joueur
            Vector3 offset = enemy.position - player.position;
            Vector2 radarPos = new Vector2(offset.x, offset.z);

            // Mise à l'échelle de la position sur le radar
            radarPos = radarPos / radarRange * (radarUI.rect.width / 2);

            // Ajuste la position de l'indicateur dans l'UI radar
            enemyIndicator.rectTransform.anchoredPosition = radarPos;

            // Affiche le point rouge
            enemyIndicator.enabled = true;
        }
        else
        {
            // Cache le point rouge si l'IA est hors de portée
            enemyIndicator.enabled = false;
        }
    }
}