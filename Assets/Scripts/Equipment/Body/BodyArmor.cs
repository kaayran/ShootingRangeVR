using Equipment.Interfaces;
using StructureComponents;
using UnityEngine;

namespace Equipment.Body
{
    public class BodyArmor : MonoBehaviour, IBody
    {
        [SerializeField] private string _name;

        private Attachment _attachment;
        private Rigidbody _rb;

        private void Start()
        {
            _attachment = GetComponent<Attachment>();
            _rb = GetComponent<Rigidbody>();

            _attachment.Init();
        }

        public void Equip(Transform slot)
        {
            transform.parent = slot;
            transform.position = slot.position;
            transform.rotation = slot.rotation;
            _rb.isKinematic = true;
        }

        public void UnEquip()
        {
            transform.parent = null;
            _rb.isKinematic = false;
        }

        public Attachment GetAttachment()
        {
            return _attachment;
        }

        public string GetBodyEquipmentName()
        {
            return _name;
        }
    }
}