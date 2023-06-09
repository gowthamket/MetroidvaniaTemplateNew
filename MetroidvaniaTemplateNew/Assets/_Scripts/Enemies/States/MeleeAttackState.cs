﻿using System.Collections;
using System.Collections.Generic;
using Scripts.Combat.Interfaces;
using Scripts.Weapons;
using UnityEngine;

public class MeleeAttackState : AttackState
{
    protected D_MeleeAttack stateData;

    private Movement Movement { get => movement ?? core.GetCoreComponent(ref movement); }
    private Movement movement;

    private DamageData damageData;

    public MeleeAttackState(Entity etity, FiniteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttack stateData) : base(etity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);

        foreach (Collider2D collider in detectedObjects)
        {
            IDamageable damageable = collider.GetComponent<IDamageable>();

            if(damageable != null)
            {
                damageData.SetData(core.Parent, stateData.attackDamage);
                damageable.Damage(damageData);
            }

            IKnockbackable knockbackable = collider.GetComponent<IKnockbackable>();

            if(knockbackable != null)
            {
                var data = new KnockbackData(stateData.knockbackAngle, stateData.knockbackStrength,
                    Movement.FacingDirection, core.transform.parent.gameObject);
                knockbackable.Knockback(data);
            }

            IPoiseDamageable poiseDamageable = collider.GetComponent<IPoiseDamageable>();

            if (poiseDamageable != null)
            {
                var data = new PoiseDamageData();
                data.Source = core.transform.parent.gameObject;
                data.PoiseDamageAmount = stateData.poiseDamage;
                poiseDamageable.PoiseDamage(data);
            }
        }
    }
}
