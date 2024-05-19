using Lance.CoreSystem;
using Lance.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public class MovementData : ComponentData<AttackMovement>
    {
        public MovementData()
        {
            ComponentDependency = typeof(Movement);
        }
    }
}
