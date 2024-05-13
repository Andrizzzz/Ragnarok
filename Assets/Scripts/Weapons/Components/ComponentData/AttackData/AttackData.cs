using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lance.Assets.Scripts.Weapons.Components.ComponentData.AttackData
{
    public class AttackData
    {
        [SerializeField, HideInInspector] private string name;

        public void SetAttackName(int i) => name = $"Attack {i}";
    }
}
