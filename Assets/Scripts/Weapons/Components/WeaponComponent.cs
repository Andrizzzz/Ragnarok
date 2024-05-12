using _Lance.Weapons;
using Lance.CoreSystem;
using Lance.Weapons;
using Lance.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lance
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;

        //protected AnimationEventHandler eventHandler => weapon.eventHandler;
        protected AnimationEventHandler eventHandler;

        protected Core Core => weapon.Core;

        protected bool isAttackActive;

        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();

            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnEnable()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void OnDisable()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }
    }

    public abstract class WeaponComponent<T> : WeaponComponent where T : ComponentData
    {

    }
}
