using Lance.CoreSystem;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Stats playerStats; // Reference to the player's Stats script
    [SerializeField] private Image healthBarFill; // Reference to the Image component for the health bar fill

    private float maxHealth; // Store the max health value

    private void Start()
    {
        if (playerStats == null)
        {
            Debug.LogError("PlayerStats reference is missing!");
            return;
        }

        if (healthBarFill == null)
        {
            Debug.LogError("HealthBarFill reference is missing!");
            return;
        }

        // Subscribe to the health change event
        playerStats.OnHealthZero += HandleHealthZero;
        playerStats.OnHealthChanged += UpdateHealthBar;

        // Store the max health value
        maxHealth = playerStats.MaxHealth;
    }

    private void HandleHealthZero()
    {
        Debug.Log("Player health reached zero!");
        // Additional logic for when health reaches zero can go here
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        float fillAmount = currentHealth / maxHealth;
        healthBarFill.fillAmount = fillAmount;
    }

    // Method to reset the health bar fill to max
    private void ResetHealthBar()
    {
        healthBarFill.fillAmount = 1f;
    }

    private void OnEnable()
    {
        // Listen for the respawn event
        GameManager.OnPlayerRespawn += ResetHealthBar;
    }

    private void OnDisable()
    {
        // Stop listening for the respawn event
        GameManager.OnPlayerRespawn -= ResetHealthBar;
    }
}
