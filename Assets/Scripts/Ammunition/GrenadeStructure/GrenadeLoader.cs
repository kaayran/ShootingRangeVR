using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeLoader : MonoBehaviour
    {
        [SerializeField] private Transform _placement;

        private Container<GrenadeFuse, GrenadeFuseType> _container;
        private Attachment _attachment;
        private GrenadeFuse _fuse;
        private Collider _collider;

        public void Init(Container<GrenadeFuse, GrenadeFuseType> container, Attachment attachment, Collider col)
        {
            _container = container;
            _attachment = attachment;
            _collider = col;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_fuse != null) return;
            if (!other.transform.root.TryGetComponent<GrenadeFuse>(out var fuse)) return;
            if (!fuse.GetAttachment().TryGetHand(out var hand)) return;

            _fuse = fuse;

            var attachment = _fuse.GetAttachment();
            attachment.OnDrop += OnDrop;
        }

        private void OnDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var fuseReqs = _container.GetStoredType().FuseName;
            var fuseName = _fuse.GetFuseType().FuseName;
            if (!fuseReqs.Equals(fuseName)) return;

            if (!_container.TryPush(_fuse)) return;

            var attachment = _fuse.GetAttachment();
            attachment.OnDrop -= OnDrop;

            var fuseTransform = _fuse.transform;
            fuseTransform.parent = _placement.transform;
            fuseTransform.position = _placement.position;
            fuseTransform.rotation = _placement.rotation;
            _fuse.GetRigidbody().isKinematic = true;

            var fuseColliders = _fuse.GetColliders();
            foreach (var fuseCollider in fuseColliders) Physics.IgnoreCollision(_collider, fuseCollider);

            _fuse.Deactivate();
            _fuse.SetLeverAttachment(_attachment);

            _fuse = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_fuse == null) return;
            if (!other.transform.root.TryGetComponent<GrenadeFuse>(out var grenadeFuse)) return;
            if (_fuse != grenadeFuse) return;

            var magazineAttachment = _fuse.GetAttachment();
            magazineAttachment.OnDrop -= OnDrop;
            _fuse = null;
        }
    }
}