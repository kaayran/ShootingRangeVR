using System;
using Audio;
using Equipment.Interfaces;
using UnityEngine;
using UnityEngine.Audio;

namespace Equipment
{
    public class HeadVisualizer : EquipmentVisualizer<IHead>
    {
        private IHead _head;

        public override void Init(AudioMixer mixer)
        {
            // Init!
            Mixer = mixer;
            Slot = new HeadSlot();
            Slot.Init(transform);
        }

        private protected override void OnTriggerStay(Collider other)
        {
            if (_head != null) return;
            if (!other.TryGetComponent<IHead>(out var helmet)) return;
            if (!helmet.GetAttachment().TryGetHand(out var hand)) return;

            _head = helmet;

            var attachment = _head.GetAttachment();
            attachment.OnDrop += EquipmentDrop;
        }

        private protected override void EquipmentDrop()
        {
            if (!Slot.IsSlotAvailable()) return;

            var attachment = _head.GetAttachment();
            attachment.OnDrop -= EquipmentDrop;
            attachment.OnTake += EquipmentTake;

            _head.Equip(Slot.GetEquipSlotTransform());
            Slot.SetEquipmentInSlot(_head);
            
            AudioManager.Instance.MixerHelmetOn(_head.Suppression);

            _head = null;
        }

        private protected override void EquipmentTake()
        {
            Slot.RemoveEquipmentInSlot(out var removed);
            removed.GetAttachment().OnTake -= EquipmentTake;
            
            AudioManager.Instance.MixerHelmetOn(0f);

            removed.UnEquip();
        }

        private protected override void OnTriggerExit(Collider other)
        {
            if (_head == null) return;

            if (!other.TryGetComponent<IHead>(out var helmet)) return;
            if (_head != helmet) return;

            var attachment = _head.GetAttachment();
            attachment.OnDrop -= EquipmentDrop;

            _head = null;
        }
    }
}