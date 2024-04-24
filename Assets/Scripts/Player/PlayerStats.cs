using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private GameObject deathChunkParticle;
    [SerializeField]
    private GameObject deathBloodParticle;
    public float maxHealth;
    private float currentHealth;
    private GameManager GM;

    private void Start()
    {
        currentHealth = maxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Accessor for current health
    public float GetCurrentHealth()
    {
        return currentHealth;
    }


    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount; // Subtract amount from current health

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
