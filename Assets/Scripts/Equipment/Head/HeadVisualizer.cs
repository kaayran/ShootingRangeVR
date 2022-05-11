using Audio;
using Equipment.Interfaces;
using UnityEngine;

namespace Equipment.Head
{
    public class HeadVisualizer : EquipmentVisualizer<IHead>
    {
        private IHead _head;

        public virtual void Init()
        {
            // Init!
            Slot = new HeadSlot();
            Slot.Init(transform);
        }

        private protected override void OnTriggerStay(Collider other)
        {
            if (_head != null) return;
            if (!other.TryGetComponent<IHead>(out var headEquipment)) return;
            if (!headEquipment.GetAttachment().TryGetHand(out _)) return;

            _head = headEquipment;

            var attachment = _head.GetAttachment();
            attachment.OnDrop += EquipmentDrop;
        }

        private protected virtual void EquipmentDrop()
        {
            if (!Slot.IsSlotAvailable()) return;

            var attachment = _head.GetAttachment();
            attachment.OnDrop -= EquipmentDrop;
            attachment.OnTake += EquipmentTake;

            _head.Equip(Slot.GetEquipSlotTransform());
            Slot.SetEquipmentInSlot(_head);

            AudioManager.Instance.MixerHelmetOn(_head.GetHelmetSuppression());

            _head = null;
        }

        private protected virtual void EquipmentTake()
        {
            Slot.RemoveEquipmentInSlot(out var headEquipment);
            headEquipment.GetAttachment().OnTake -= EquipmentTake;

            AudioManager.Instance.MixerDefault();

            headEquipment.UnEquip();
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