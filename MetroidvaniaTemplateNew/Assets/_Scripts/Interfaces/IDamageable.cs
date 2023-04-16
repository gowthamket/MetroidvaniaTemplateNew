﻿using System.Collections;
using System.Collections.Generic;
using Scripts.Weapons;
using UnityEngine;

namespace Scripts.Combat.Interfaces
{
    public interface IDamageable
    {
        void Damage(DamageData data);
    }
 
    public struct DamageData
    {
        public float DamageAmount;
        public GameObject Source;

        public void SetData(GameObject source, float damageAmount)
        {
            DamageAmount = damageAmount;
            Source = source;
        }
    }
}