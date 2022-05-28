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
        private BodyAudio _bodyAudio;

        private void Start()
        {
            _attachment = GetComponent<Attachment>();
            _rb = GetComponent<Rigidbody>();
            _bodyAudio = GetComponent<BodyAudio>();

            _attachment.Init();
        }

        public void Equip(Transform slot)
        {
            var transformSelf = transform;
            transformSelf.parent = slot;
            transformSelf.position = slot.position;
            transformSelf.rotation = slot.rotation;
            _rb.isKinematic = true;

            _bodyAudio.PlayEquipSound();
        }

        public void UnEquip()
        {
            transform.parent = null;
            _rb.isKinematic = false;

            _bodyAudio.PlayUnEquipSound();
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