using Lance.Weapons.Components.ComponentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class MovementData : ComponentData
    {
        [field: SerializeField] public AttackMovement[] AttackData { get; private set; }
    }
}
