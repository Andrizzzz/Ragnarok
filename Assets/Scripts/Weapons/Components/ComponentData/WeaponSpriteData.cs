using Lance.Weapons.Components.ComponentData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class WeaponSpriteData : ComponentData
    {
        [field: SerializeField] public AttackSprites[] AttackData { get; private set; }

    }
}
