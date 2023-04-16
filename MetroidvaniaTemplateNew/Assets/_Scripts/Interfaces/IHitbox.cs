using System;
using UnityEngine;

namespace Scripts.Combat.Interfaces
{
    public interface IHitbox
    {
        event Action<RaycastHit2D[]> OnDetected;
    }
}