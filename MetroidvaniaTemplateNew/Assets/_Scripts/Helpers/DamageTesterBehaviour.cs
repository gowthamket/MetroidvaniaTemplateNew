﻿using System;
using System.Net.Http.Headers;
using Scripts.Combat;
using Scripts.Combat.Interfaces;
using Scripts.Weapons;
using UnityEngine;

namespace Scripts._Scripts.BardentHelpers
{
    public class DamageTesterBehaviour : MonoBehaviour
    {
        public Rect Hitbox;
        public LayerMask damageable;
        public string debugString;
        public string button;

        private DamageData damageData;
        
        private void CheckDamage()
        {
            var hits = Physics2D.OverlapBoxAll(transform.position, Hitbox.size, 0f, damageable);
            foreach (var item in hits)
            {
                damageData.SetData(gameObject, 1f);
                CombatUtilities.CheckIfDamageable(item, damageData, out _);
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Check Damage"))
            {
                print(debugString);
                CheckDamage();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, Hitbox.size);
        }
    }
}