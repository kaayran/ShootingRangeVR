using System;
using Equipment.Interfaces;
using StructureComponents;
using UnityEngine;

namespace Equipment
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Rigidbody))]
    public class Helmet : MonoBehaviour, IHelmet
    {
        [SerializeField] private string _name;

        private Attachment _attachment;
        private Rigidbody _rb;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _rb = GetComponent<Rigidbody>();
            _attachment = GetComponent<Attachment>();
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

        public string GetHelmetName()
        {
            return _name;
        }
    }
}