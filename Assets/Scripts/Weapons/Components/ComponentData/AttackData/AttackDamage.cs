using System;

using UnityEngine;

namespace Lance.Weapons.Components
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public float Amount { get;private set; }
    }
}
