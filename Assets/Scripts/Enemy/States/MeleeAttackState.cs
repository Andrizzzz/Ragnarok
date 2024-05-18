using Lance.CoreSystem;
using Lance.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : AttackState
{
   protected D_MeleeAttack stateData;
   protected AttackDetails attackDetails;

    public MeleeAttackState(Entity entity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails = new AttackDetails();

        attackDetails.damageAmount = stateData.attackDamage;
        attackDetails.position = entity.transform.position;
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if (damageable != null)
            {
                damageable.Damage(stateData.attackDamage);
            }

            //IKnockBackable knockBackable = collider.GetComponent<IKnockBackable>();

            //if (knockBackable != null)
            //{
            //    knockBackable.KnockBack(stateData.knockbackAngle, stateData.knockbackStrength, core.Movement.FacingDirection);
            //}
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
