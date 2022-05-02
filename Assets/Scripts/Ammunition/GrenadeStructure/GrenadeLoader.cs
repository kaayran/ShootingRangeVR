using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeLoader : MonoBehaviour
    {
        [SerializeField] private Transform _placement;
        
        private Container<GrenadeFuse, GrenadeFuseType> _grenadeContainer;
        private Attachment _attachment;
        private GrenadeFuse _grenadeFuse;
        private Collider _collider;

        public void Init(Container<GrenadeFuse, GrenadeFuseType> container, Attachment attachment, Collider collider)
        {
            _grenadeContainer = container;
            _attachment = attachment;
            _collider = collider;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_grenadeFuse != null) return;
            if (!other.transform.root.TryGetComponent<GrenadeFuse>(out var fuse)) return;
            if (!fuse.GetAttachment().TryGetHand(out var hand)) return;

            _grenadeFuse = fuse;

            var attachment = _grenadeFuse.GetAttachment();
            attachment.OnDrop += OnDrop;
        }

        private void OnDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var fuseReqs = _grenadeContainer.GetStoredType().FuseName;
            var fuseName = _grenadeFuse.GetFuseType().FuseName;
            if (!fuseReqs.Equals(fuseName)) return;

            if (!_grenadeContainer.TryPush(_grenadeFuse)) return;

            var attachment = _grenadeFuse.GetAttachment();
            attachment.OnDrop -= OnDrop;

            _grenadeFuse.transform.parent = _placement.transform;
            _grenadeFuse.transform.position = _placement.position;
            _grenadeFuse.transform.rotation = _placement.rotation;
            _grenadeFuse.GetRigidbody().isKinematic = true;

            var fuseColliders = _grenadeFuse.GetComponentsInChildren<Collider>();
            foreach (var fuseCollider in fuseColliders)
            {
                Physics.IgnoreCollision(_collider, fuseCollider);
            }
            
            _grenadeFuse = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_grenadeFuse == null) return;
            if (!other.transform.root.TryGetComponent<GrenadeFuse>(out var grenadeFuse)) return;
            if (_grenadeFuse != grenadeFuse) return;

            var magazineAttachment = _grenadeFuse.GetAttachment();
            magazineAttachment.OnDrop -= OnDrop;
            _grenadeFuse = null;
        }
    }
}