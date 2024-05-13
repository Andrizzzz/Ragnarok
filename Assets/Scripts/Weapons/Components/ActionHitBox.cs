using Lance.Assets.Scripts.Weapons.Components.ComponentData;
using Lance.CoreSystem;
using Lance.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance.Weapons.Components
{
    public class ActionHitBox : WeaponComponent<ActionHitBoxData, AttackActionHitBox>
    {
        private CoreComp<CoreSystem.Movement> movement;

        private void HandleAttackAction()
        {
            Debug.Log(movement.Comp.FacingDirection);
        }

        protected override void Start()
        {
            base.Start();

            movement = new CoreComp<CoreSystem.Movement>(Core);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            eventHandler.OnAttackAction += HandleAttackAction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            eventHandler.OnAttackAction -= HandleAttackAction;
        }
    }
}
