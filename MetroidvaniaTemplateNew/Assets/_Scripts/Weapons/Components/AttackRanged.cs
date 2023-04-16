using System.Collections;
using System.Collections.Generic;
using Scripts.Utilities;
using Scripts.Projectiles;
using UnityEngine;
using System;

namespace Scripts.Weapons
{
    public class AttackRanged : WeaponComponent<AttackRangedData>
    {
        public event Action<GameObject> OnProjectileSpawned;

        public event Func<int, int> OnSetNumberOfProjectiles;
        public event Func<Vector2, Vector2[]> OnSetProjectileDirection;

        private Vector2 offset;
        private Vector2 direction;

        private Movement Movement => movement ?? core.GetCoreComponent(ref movement);
        private Movement movement;

        private Transform projectileContainer;

        private int numberToSpawn = 1;

        private void SpawnProjectiles()
        {
            var curAtkData = data.GetAttackData(counter);

            foreach (var point in curAtkData.AttackData)
            {
                int num2spwn = OnSetNumberOfProjectiles?.Invoke(numberToSpawn) ?? numberToSpawn;

                var position = transform.position;
                offset.Set(
                    position.x + point.offset.x * Movement.FacingDirection,
                    position.y + point.offset.y
                );

                direction.Set(point.direction.x * Movement.FacingDirection, point.direction.y);

                var directions = OnSetProjectileDirection?.Invoke(direction) ?? new Vector2[] { direction };

                for (int i = 0; (i < num2spwn) || (i < directions.Length); i++)
                {
                    var projectile = Instantiate(
                        point.projectileData.ProjectilePrefab,
                        offset,
                        Quaternion.Euler(0f, 0f, Utilities.VectorUtilities.AngleFromVector2(directions[i])),
                        projectileContainer);

                    var projectileScript = projectile.GetComponent<Projectile>();
                    projectileScript.CreateProjectile(point.projectileData);

                    OnProjectileSpawned?.Invoke(projectile);

                    projectileScript.Init(core.Parent);
                }
            }
        }

        public override void SetReferences()
        {
            base.SetReferences();
            projectileContainer = GameObject.FindGameObjectWithTag("ProjectileContainer").transform;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            eventHandler.OnAttackAction += SpawnProjectiles;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            eventHandler.OnAttackAction -= SpawnProjectiles;
        }

        private void OnDrawGizmos()
        {
            var weaponScript = GetComponent<Weapon>();
            if (weaponScript == null) return;

            var allData = weaponScript.WeaponData.GetComponentData<AttackRangedData>().GetAllData();

            if (allData == null) return;

            foreach (var item in allData)
            {
                if (!item.debug) continue;
                foreach (var point in item.AttackData)
                {
                    var pos = transform.position + (Vector3)point.offset;

                    Gizmos.DrawWireSphere(pos, 0.2f);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(pos, pos + (Vector3)point.direction.normalized);
                    Gizmos.color = Color.white;
                }
            }
        }
    }

    [Serializable]
    public class AttackRangedData : WeaponComponentData<RangedData>
    {
        public AttackRangedData() => ComponentDependencies.Add(typeof(AttackRanged));
    }
}