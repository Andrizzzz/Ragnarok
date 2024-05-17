using System;
using UnityEngine;

namespace Lance.Weapons.Components
{
    [Serializable]
    public class DamageData : ComponentData<AttackDamage>
    {
        [SerializeField] private float amount;
        [SerializeField] private GameObject source;

        public float Amount { get => amount; private set => amount = value; }
        public GameObject Source { get => source; private set => source = value; }

        public DamageData(float amount, GameObject source)
        {
            this.amount = amount;
            this.source = source;
        }

        public void SetAmount(float amount)
        {
            this.amount = amount;
        }
    }
}
