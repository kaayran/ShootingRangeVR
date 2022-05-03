using System.Collections.Generic;
using Interfaces;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    [RequireComponent(typeof(GrenadeFuseExploder))]
    [RequireComponent(typeof(GrenadeFuseStriker))]
    [RequireComponent(typeof(CollisionIgnoring))]
    [RequireComponent(typeof(Attachment))]
    public class GrenadeFuse : MonoBehaviour, IActivatable
    {
        [SerializeField] private GrenadeFuseExploderParticle _fuseExploderParticle;
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
            _fuseExploderParticle.Init(_fuseExploder);

            _fuseExploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            _fuseExploder.OnDetonate -= Detonate;
        }

        public void Activate()
        {
            _rigidbody.velocity = Vector3.up;
            _rigidbody.angularVelocity = Vector3.zero;

            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            _attachment.TryGetHand(out var hand);
            hand.DetachObject(gameObject, false);
        }

        public Attachment GetAttachment()
        {
            return _attachment;
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