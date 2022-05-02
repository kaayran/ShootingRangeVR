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
        [SerializeField] private GrenadeFuseExploderView _fuseExploderView;
        [SerializeField] private GrenadeFuseLever _fuseLever;
        [SerializeField] private GrenadeFuseRing _fuseRing;
        [SerializeField] private GrenadeFuseType _fuseType;

        private GrenadeFuseExploder _fuseExploder;
        private GrenadeFuseStriker _fuseStriker;

        private CollisionIgnoring _collisionIgnoring;
        private Attachment _attachment;

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            _collisionIgnoring = GetComponent<CollisionIgnoring>();
            _attachment = GetComponent<Attachment>();

            _collisionIgnoring.Init();
            _attachment.Init();

            _fuseExploder = GetComponent<GrenadeFuseExploder>();
            _fuseStriker = GetComponent<GrenadeFuseStriker>();

            _fuseRing.Init();
            _fuseLever.Init(_attachment);
            _fuseStriker.Init(_fuseLever, _fuseRing);
            _fuseExploder.Init(_fuseStriker);
            _fuseExploderView.Init(_fuseExploder);

            _fuseExploder.OnExplosion += OnExplosion;
        }

        private void OnExplosion()
        {
            Deactivate();
            _fuseExploder.OnExplosion -= OnExplosion;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public Attachment GetAttachment()
        {
            return _attachment;
        }

        public GrenadeFuseType GetFuseType()
        {
            return (GrenadeFuseType) _fuseType.Clone();
        }
    }
}