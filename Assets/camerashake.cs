using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform enemy;
    public Transform player;
    public float maxShakeMagnitude = 0.5f;
    public float shakeDistance = 10f;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, enemy.position);
        if (distance < shakeDistance)
        {
            float shakeAmount = maxShakeMagnitude * (1 - (distance / shakeDistance));
            Vector3 shakePosition = initialPosition + Random.insideUnitSphere * shakeAmount;
            transform.localPosition = shakePosition;
        }
        else
        {
            transform.localPosition = initialPosition;
        }
    }
}
