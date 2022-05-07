using StructureComponents;
using UnityEngine;

namespace Equipment.Interfaces
{
    public interface IEquippable
    {
        public void Equip(Transform slot);
        public void UnEquip();
        public Attachment GetAttachment();
    }
}