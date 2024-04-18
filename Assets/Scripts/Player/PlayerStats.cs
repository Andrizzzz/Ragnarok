using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float MaxHealth;

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private float currentHealth;

    private GameManager GM;

    private void Start()
    {
        currentHealth = MaxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Change the access modifier to public
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(deathChunkParticle, transform.position, Quaternion.identity);
        Instantiate(deathBloodParticle, transform.position, Quaternion.identity);
        GM.Respawn();
        Destroy(gameObject);
    }
}
