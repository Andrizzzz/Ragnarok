using Lance.Assets.Scripts.Weapons.Components.ComponentData.AttackData;
using System;
using UnityEngine;

namespace Lance.Weapons.Components
{

    [Serializable]
    public class ComponentData
    {
        [SerializeField] private string name;

        public void SetComponentName() => name = GetType().Name;

    }

    [Serializable]

    public class ComponentData<T> : ComponentData where T: AttackData 
    {
        [field: SerializeField] public T[] AttackData { get; private set; }
    }

}
