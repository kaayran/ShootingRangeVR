using Equipment.Interfaces;
using UnityEngine;

namespace Equipment
{
    public class HelmetSlot : EquipmentSlot<IHelmet>
    {
        public override void Init()
        {
            Equipment = null;
            Transform = GetComponent<Transform>();
        }

        public override Transform GetEquipSlotTransform()
        {
            return Transform;
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