using Lance.Assets.Scripts.Weapons.Components.ComponentData.AttackData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    [Serializable]
    public class AttackMovement : AttackData
    {
       [field: SerializeField] public Vector2 Direction { get; private set; }
       [field: SerializeField] public float Velocity { get; private set; }

    }
}
