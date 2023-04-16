﻿using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Combat.Interfaces;
using Scripts.Modifiers;
using Scripts.Weapons;
using UnityEngine;

public class DamageComponent : CoreComponent, IDamageable
{
    public event Action<GameObject> OnDamage;

    [SerializeField] private GameObject damageParticles;

    public ModifierContainer<DamageModifier, DamageData> DamageModifiers { get; private set; } =
        new ModifierContainer<DamageModifier, DamageData>();

    private Stats Stats => stats ? stats : core.GetCoreComponent(ref stats);

    private CollisionSenses CollisionSenses => collisionSenses ?? core.GetCoreComponent(ref collisionSenses);

    private Movement Movement => movement ?? core.GetCoreComponent(ref movement);

    private ParticleManager ParticleManager => particleManager ?? core.GetCoreComponent(ref particleManager);

    private Movement movement;
    private Stats stats;
    private CollisionSenses collisionSenses;
    private ParticleManager particleManager;

    public void Damage(DamageData data)
    {
        
        OnDamage?.Invoke(data.Source);

        var modifiedData = DamageModifiers.ApplyModifiers(data);
        print($"{core.Parent.name} Damage by {modifiedData.DamageAmount}");

        if (modifiedData.DamageAmount <= 0.0f) return;

        Stats?.Health.Decrease(modifiedData.DamageAmount);
        ParticleManager?.StartParticlesWithRandomRotation(damageParticles);
    }
}