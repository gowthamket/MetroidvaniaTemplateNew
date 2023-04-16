using Scripts.Projectiles;
using UnityEngine;

namespace Scripts.Weapons
{
  [System.Serializable]
  public struct WeaponProjectileSpawnPoint
  {
    public Vector2 offset;
    public Vector2 direction;
    public ProjectileDataSO projectileData;
  }
}