using UnityEngine;

namespace Equipment.Interfaces
{
    public class HelmetSlot : EquipmentSlot<IHelmet>
    {
        public override void Init()
        {
            Equipment = null;
        }

        public override Transform GetEquipSlotTransform()
        {
            return _transform;
        }

        public override void SetEquipmentInSlot(IHelmet equipment)
        {
            Equipment = equipment;
        }

        public override bool IsSlotAvailable()
        {
            return Equipment == null;
        }
    }
}