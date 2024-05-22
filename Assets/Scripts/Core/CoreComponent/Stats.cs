using System;
using UnityEngine;

namespace Lance.CoreSystem
{
    public class Stats : CoreComponent
    {
        public event Action OnHealthZero;
        public event Action<float, float> OnHealthChanged; // Event to notify listeners of health changes

        [SerializeField] private float maxHealth;
        private float currentHealth;

        protected override void Awake()
        {
            base.Awake();

            currentHealth = maxHealth;
        }

        public void DecreaseHealth(float amount)
        {
            currentHealth -= amount;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

            OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify listeners of health change

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnHealthZero?.Invoke();
                Debug.Log("Health is zero!!");
            }
        }

        public void IncreaseHealth(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            Debug.Log("Health increased to: " + currentHealth);
            OnHealthChanged?.Invoke(currentHealth, maxHealth); // Notify listeners of health change
        }
    }
}
