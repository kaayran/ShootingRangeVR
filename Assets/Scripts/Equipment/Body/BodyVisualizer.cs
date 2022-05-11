using Equipment.Interfaces;
using UnityEngine;

namespace Equipment.Body
{
    public class BodyVisualizer : EquipmentVisualizer<IBody>
    {
        private IBody _body;

        public virtual void Init()
        {
            _body = null;
            Slot = new BodySlot();
            Slot.Init(transform);
        }

        private protected override void OnTriggerStay(Collider other)
        {
            if (_body != null) return;
            if (!other.TryGetComponent<IBody>(out var bodyEquipment)) return;
            if (!bodyEquipment.GetAttachment().TryGetHand(out _)) return;

            _body = bodyEquipment;

            var attachment = _body.GetAttachment();
            attachment.OnDrop += EquipmentDrop;
        }

        private protected override void OnTriggerExit(Collider other)
        {
            if (_body == null) return;

            if (!other.TryGetComponent<IBody>(out var bodyEquipment)) return;
            if (_body != bodyEquipment) return;

            var attachment = _body.GetAttachment();
            attachment.OnDrop -= EquipmentDrop;

            _body = null;
        }

        private protected virtual void EquipmentDrop()
        {
            if (!Slot.IsSlotAvailable()) return;

            var attachment = _body.GetAttachment();
            attachment.OnDrop -= EquipmentDrop;
            attachment.OnTake += EquipmentTake;

            _body.Equip(Slot.GetEquipSlotTransform());
            Slot.SetEquipmentInSlot(_body);

            _body = null;
        }

        private protected virtual void EquipmentTake()
        {
            Slot.RemoveEquipmentInSlot(out var headEquipment);
            headEquipment.GetAttachment().OnTake -= EquipmentTake;

            headEquipment.UnEquip();
        }
    }
}