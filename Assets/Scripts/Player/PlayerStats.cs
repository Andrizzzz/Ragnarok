using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
<<<<<<< HEAD
    private float MaxHealth;

    [SerializeField]
    private GameObject
        deathChunkParticle,
        deathBloodParticle;

    private float currentHealth;

=======
    private GameObject deathChunkParticle;
    [SerializeField]
    private GameObject deathBloodParticle;
    public float maxHealth;
    private float currentHealth;
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
    private GameManager GM;

    private void Start()
    {
<<<<<<< HEAD
        currentHealth = MaxHealth;
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Change the access modifier to public
    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount;
=======
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
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
<<<<<<< HEAD
        Instantiate(deathChunkParticle, transform.position, Quaternion.identity);
        Instantiate(deathBloodParticle, transform.position, Quaternion.identity);
        GM.Respawn();
        Destroy(gameObject);
=======
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        gameObject.SetActive(false);
        //Destroy(gameObject);
>>>>>>> f9594ac2c9a4404ac333ca3aa6d1904365082f39
    }
}
