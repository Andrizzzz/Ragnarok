using Lance.Assets.Scripts.Weapons.Components.ComponentData.AttackData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    [Serializable]
    public class AttackSprites : AttackData
    {
        [field: SerializeField] public Sprite[] Sprites { get; private set; }
    }
}
