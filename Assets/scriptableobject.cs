using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProgressionData", menuName = "Enemy/ProgressionData")]
public class EnemyProgressionData : ScriptableObject
{
    public float detectionRange;
    public float fieldOfViewAngle;
    public float walkSpeed;
    public float runSpeed;
}
