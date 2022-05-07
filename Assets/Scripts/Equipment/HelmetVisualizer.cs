using Equipment.Interfaces;
using UnityEngine;

namespace Equipment
{
    public class HelmetVisualizer : EquipmentVisualizer<IHelmet>
    {
        private IHelmet _helmet;

        public override void Init()
        {
            // Init!
            _slot.Init();
        }

        private protected override void OnTriggerStay(Collider other)
        {
            if (_helmet != null) return;
            if (!other.TryGetComponent<IHelmet>(out var helmet)) return;
            if (!helmet.GetAttachment().TryGetHand(out var hand)) return;

            _helmet = helmet;

            var attachment = _helmet.GetAttachment();
            attachment.OnDrop += OnEquipmentDrop;
        }

        private protected override void OnEquipmentDrop()
        {
            if (!_slot.IsSlotAvailable()) return;

            var attachment = _helmet.GetAttachment();
            attachment.OnDrop -= OnEquipmentDrop;

            _helmet.Equip(_slot.GetEquipSlotTransform());
            _slot.SetEquipmentInSlot(_helmet);

            _helmet = null;
        }

        private protected override void OnTriggerExit(Collider other)
        {
            if (_helmet == null) return;

            if (!other.TryGetComponent<IHelmet>(out var helmet)) return;
            if (_helmet != helmet) return;

            var attachment = _helmet.GetAttachment();
            attachment.OnDrop -= OnEquipmentDrop;

            _helmet = null;
        }
    }
}