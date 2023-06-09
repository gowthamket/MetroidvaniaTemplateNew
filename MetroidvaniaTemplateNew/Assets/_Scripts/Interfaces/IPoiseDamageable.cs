﻿using System;
using UnityEngine;

namespace Scripts.Combat.Interfaces
{
    public interface IPoiseDamageable
    {
        void PoiseDamage(PoiseDamageData data);
    }

    public struct PoiseDamageData
    {
        public float PoiseDamageAmount;
        public GameObject Source;

        public void SetData(GameObject source, float poiseDamageAmount)
        {
            Source = source;
            PoiseDamageAmount = poiseDamageAmount;
        }
    }
}