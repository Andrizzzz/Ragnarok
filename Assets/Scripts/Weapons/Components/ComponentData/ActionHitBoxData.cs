using Lance.Weapons.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Lance.Assets.Scripts.Weapons.Components.ComponentData
{
    public class ActionHitBoxData : ComponentData<AttackActionHitBox>
    {
        [field: SerializeField] public LayerMask DetectableLayers { get; private set; }

        //protected override void SetComponentDependency()
        //{
        //    ComponentDependency = typeof(ActionHitBox);
        //}
    }
}
