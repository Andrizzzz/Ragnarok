using System;
using UnityEngine;

namespace Lance.CoreSystem
{
    public class Stats : CoreComponent
    {
        public event Action OnHealthZero;
        public event Action<float, float> OnHealthChanged;

        [SerializeField] private float maxHealth;
        [SerializeField] private bool isPlayer;  // Add this to identify if this is the player
        public float currentHealth;
        private GameManager GM;

        public float MaxHealth => maxHealth;

        protected override void Awake()
        {
            base.Awake();
            Load(); // Load saved health on awake
            GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        public void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnHealthZero?.Invoke();
                Debug.Log("Health is zero!!");

                Die();
            }

            Save(); // Save health after changing
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log("Health increased to: " + currentHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);

            Save(); // Save health after changing
        }

        private void Die()
        {
            if (isPlayer)
            {
                currentHealth = maxHealth; // Reset current health to max health
                GM.Respawn(); // Only respawn if this is the player
            }
            gameObject.SetActive(false);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat("CurrentHealth", currentHealth);
            PlayerPrefs.Save();
        }

        private void Load()
        {
            currentHealth = PlayerPrefs.GetFloat("CurrentHealth", maxHealth);
        }

       
    }
}
