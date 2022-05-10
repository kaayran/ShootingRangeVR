﻿using Equipment.Interfaces;
using StructureComponents;
using UnityEngine;

namespace Equipment
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Rigidbody))]
    public class Helmet : MonoBehaviour, IHead
    {
        public float Suppression { get; private set; }
        
        [SerializeField] private string _name;
        [SerializeField] private HelmetVisor _visor;

        private CollisionIgnoring _collisionIgnoring;
        private Attachment _attachment;
        private Rigidbody _rb;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _visor.Init();
            _collisionIgnoring = GetComponent<CollisionIgnoring>();
            _attachment = GetComponent<Attachment>();
            _rb = GetComponent<Rigidbody>();

            _collisionIgnoring.Init();
            _attachment.Init();

            Suppression = -10f;
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

        public string GetHeadEquipmentName()
        {
            return _name;
        }
    }
}