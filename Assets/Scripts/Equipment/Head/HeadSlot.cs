using Equipment.Interfaces;
using UnityEngine;

namespace Equipment.Head
{
    public class HeadSlot : EquipmentSlot<IHead>
    {
        public override void Init(Transform transform)
        {
            Equipment = null;
            Transform = transform;
        }

        public override Transform GetEquipSlotTransform()
        {
            return Transform;
        }

        public override void SetEquipmentInSlot(IHead equipment)
        {
            Equipment = equipment;
        }

        public override bool IsSlotAvailable()
        {
            return Equipment == null;
        }

        public override void RemoveEquipmentInSlot(out IHead removed)
        {
            removed = Equipment;
            Equipment = null;
        }
    }
}