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
            currentHealth = maxHealth;
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
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log("Health increased to: " + currentHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        private void Die()
        {
            if (isPlayer)
            {
                GM.Respawn(); // Only respawn if this is the player
            }
            gameObject.SetActive(false);
        }
    }
}
