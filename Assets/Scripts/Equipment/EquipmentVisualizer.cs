using Equipment.Interfaces;
using UnityEngine;
using UnityEngine.Audio;

namespace Equipment
{
    public abstract class EquipmentVisualizer<T> : MonoBehaviour where T : IEquippable
    {
        private protected EquipmentSlot<T> Slot;
        private protected AudioMixer Mixer;

        public abstract void Init(AudioMixer mixer);

        private protected abstract void OnTriggerStay(Collider other);
        private protected abstract void OnTriggerExit(Collider other);
        private protected abstract void EquipmentDrop();
        private protected abstract void EquipmentTake();
    }
}