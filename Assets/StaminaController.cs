using UnityEngine;

public class StaminaController : MonoBehaviour
{
    public float maxStamina = 100f; // Stamina maximale
    public float currentStamina; // Stamina actuelle
    public float staminaDrainRate = 10f; // Taux de drainage de la stamina
    public float staminaRegenRate = 5f; // Taux de régénération de la stamina

    private void Start()
    {
        currentStamina = maxStamina; // Initialiser la stamina actuelle à la valeur maximale
    }

    public bool IsSprinting()
    {
        return currentStamina > 0; // Vérifie si le joueur peut sprinter
    }

    public void DrainStamina(float amount)
    {
        currentStamina = Mathf.Clamp(currentStamina - amount, 0, maxStamina); // Draine la stamina et la garde dans les limites
    }

    public void RegenerateStamina()
    {
        currentStamina = Mathf.Clamp(currentStamina + staminaRegenRate * Time.deltaTime, 0, maxStamina); // Régénère la stamina
    }
}
