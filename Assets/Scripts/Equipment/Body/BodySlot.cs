using Equipment.Interfaces;
using UnityEngine;

namespace Equipment.Body
{
    public class BodySlot : EquipmentSlot<IBody>
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

        public override void SetEquipmentInSlot(IBody equipment)
        {
            Equipment = equipment;
        }

        public override bool IsSlotAvailable()
        {
            return Equipment == null;
        }

        public override void RemoveEquipmentInSlot(out IBody removed)
        {
            removed = Equipment;
            Equipment = null;
        }
    }
}