using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Weapons
{
    public class WeaponInputHold : WeaponComponent<WeaponHoldData>
    {
        private bool minHoldPassed;
        private bool input;

        private void HandleInputChange(bool value)
        {
            input = value;
            SetParams();
        }

        private void SetParams()
        {
            if (!input && !minHoldPassed) return;
            weapon.Anim.SetBool(WeaponBoolAnimParameters.hold.ToString(), input);
        }

        private void Enter()
        {
            minHoldPassed = false;
        }

        private void MinHoldPassed()
        {
            minHoldPassed = true;
            SetParams();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.OnInputChange += HandleInputChange;
            weapon.OnEnter += Enter;
            eventHandler.OnMinHold += MinHoldPassed;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.OnInputChange -= HandleInputChange;
            weapon.OnEnter -= Enter;
            eventHandler.OnMinHold -= MinHoldPassed;
        }
    }
    
    public class WeaponHoldData : WeaponComponentData
    {
        public WeaponHoldData()
        {
            ComponentDependencies.Add(typeof(WeaponInputHold));
        }
    }
}