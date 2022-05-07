using System;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    public abstract class EquipmentVisualizer<T> : MonoBehaviour
    {
        [SerializeField] private protected EquipmentSlot<T> _slot;

        public abstract void Init();

        private protected abstract void OnTriggerStay(Collider other);
        private protected abstract void OnTriggerExit(Collider other);
        private protected abstract void OnEquipmentDrop();
    }
}