using UnityEngine;

namespace Equipment
{
    public abstract class EquipmentSlot<T> : MonoBehaviour
    {
        [SerializeField] private protected Transform _transform;

        private protected T Equipment;

        public abstract void Init();
        public abstract Transform GetEquipSlotTransform();
        public abstract void SetEquipmentInSlot(T equipment);
        public abstract bool IsSlotAvailable();
    }
}