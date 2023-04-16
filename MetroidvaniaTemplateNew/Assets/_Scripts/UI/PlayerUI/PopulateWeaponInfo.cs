﻿using System;
using Scripts.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI
{
    public class PopulateWeaponInfo: MonoBehaviour
    {
        [SerializeField] private Image weaponIcon;
        [SerializeField] private TMP_Text weaponName;
        [SerializeField] private TMP_Text weaponDescription;

        public void SetWeaponInfo(WeaponDataSO data)
        {
            weaponIcon.sprite = data.PickupSprite;
            weaponName.text = data.WeaponName;
            weaponDescription.text = data.WeaponDescription;
        }
    }
}
