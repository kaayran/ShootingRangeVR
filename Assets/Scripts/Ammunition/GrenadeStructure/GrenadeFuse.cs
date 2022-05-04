using System.Collections.Generic;
using Interfaces;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(GrenadeFuseExploder))]
    [RequireComponent(typeof(GrenadeFuseStriker))]
    [RequireComponent(typeof(CollisionIgnoring))]
    [RequireComponent(typeof(Attachment))]
    public class GrenadeFuse : MonoBehaviour, IActivatable
    {
        [SerializeField] private GrenadeFuseExploderView _fuseExploderView;
        [SerializeField] private GrenadeFuseLever _fuseLever;
        [SerializeField] private GrenadeFuseRing _fuseRing;
        [SerializeField] private GrenadeFuseType _fuseType;

        private GrenadeFuseExploder _fuseExploder;
        private GrenadeFuseStriker _fuseStriker;

        private CollisionIgnoring _collisionIgnoring;
        private Attachment _attachment;
        private Rigidbody _rigidbody;

        private List<Collider> _colliders;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _collisionIgnoring = GetComponent<CollisionIgnoring>();
            _attachment = GetComponent<Attachment>();
            _rigidbody = GetComponent<Rigidbody>();

            _collisionIgnoring.Init();
            _attachment.Init();

            _colliders = _collisionIgnoring.GetColliders();

            _fuseExploder = GetComponent<GrenadeFuseExploder>();
            _fuseStriker = GetComponent<GrenadeFuseStriker>();

            _fuseRing.Init();
            _fuseLever.Init(_attachment);
            _fuseStriker.Init(_fuseLever, _fuseRing);
            _fuseExploder.Init(_fuseStriker);
            _fuseExploderView.Init(_fuseExploder);
        }

        public void Activate()
        {
            _rigidbody.velocity = Vector3.up;
            _rigidbody.angularVelocity = Vector3.zero;

            foreach (var col in _colliders)
            {
                if (col.transform.parent.TryGetComponent<GrenadeFuseRing>(out var component)) continue;
                var ignore = col.gameObject.GetComponent<IgnoreHovering>();
                Destroy(ignore);
            }

            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            foreach (var col in _colliders)
            {
                if (!col.transform.parent.TryGetComponent<GrenadeFuseRing>(out var component))
                {
                    col.gameObject.AddComponent<IgnoreHovering>();
                }
            }
        }

        public Attachment GetAttachment()
        {
            return _attachment;
        }

        public void SetLeverAttachment(Attachment attachment)
        {
            _fuseLever.SetAttachment(attachment);
        }

        public void RevertLeverAttachment()
        {
            _fuseLever.SetAttachment(_attachment);
        }

        public GrenadeFuseType GetFuseType()
        {
            return (GrenadeFuseType) _fuseType.Clone();
        }

        public Rigidbody GetRigidbody()
        {
            return _rigidbody;
        }

        public GrenadeFuseExploder GetExploder()
        {
            return _fuseExploder;
        }

        public List<Collider> GetColliders()
        {
            return _colliders;
        }
    }
}