using UnityEngine;

namespace Scripts.Combat.Interfaces
{
    public interface IParryable
    {
        void Parry(ParryData data);
        GameObject GetParent();
    }

    public struct ParryData
    {
        public GameObject source;
    }
}