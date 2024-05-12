using Lance.Weapons.Components.ComponentData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Lance.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public int NumberOfAttacks { get; private set; }

        [field: SerializeReference]public List<ComponentData> componentData { get; private set; }

        public T GetData<T>()
        {
            return componentData.OfType<T>().FirstOrDefault();
        }

        [ContextMenu("Add Sprite Data")]
        private void AddSpriteData() => componentData.Add(new WeaponSpriteData());
    }
}
