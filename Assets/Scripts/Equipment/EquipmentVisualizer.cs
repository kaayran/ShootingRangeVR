using Equipment.Interfaces;
using UnityEngine;

namespace Equipment
{
    public abstract class EquipmentVisualizer<T> : MonoBehaviour where T : IEquippable
    {
        private protected EquipmentSlot<T> Slot;

        public abstract void Init();

        private protected abstract void OnTriggerStay(Collider other);
        private protected abstract void OnTriggerExit(Collider other);
        private protected abstract void EquipmentDrop();
        private protected abstract void EquipmentTake();
    }
}