using Lance.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance.Weapons
{
    public class Movements : WeaponComponent
    {
        private CoreSystem.Movement coreMovement;
        //private CoreSystem.Movement CoreMovement => coreMovement ? coreMovement : Core.GetCoreComponent(ref CoreMovement);

        private MovementData data;
        private void HandleStartMovement()
        {
            var currentAttackData = data.AttackData[weapon.CurrentAttackCounter];

        }

        private void HandleStopMovement()
        {

        }

        protected override void OnEnable()
        {
            base.OnEnable();

            EventHandler.OnStartMovement += HandleStartMovement;
            EventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            EventHandler.OnStartMovement -= HandleStartMovement;
            EventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
