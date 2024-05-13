using Lance.Assets.Scripts.Weapons.Components.ComponentData.AttackData;
using System;
using UnityEngine;

namespace Lance.Weapons.Components
{

    [Serializable]
    public class ComponentData
    {

    }

    [Serializable]

    public class ComponentData<T> : ComponentData where T: AttackData 
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }

}
