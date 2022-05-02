using StructureComponents;
using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeInserter : MonoBehaviour
    {
        private GrenadeContainer _grenadeContainer;
        private Attachment _attachment;
        private GrenadeFuse _grenadeFuse;

        public void Init(GrenadeContainer grenadeContainer, Attachment attachment)
        {
            _grenadeContainer = grenadeContainer;
            _attachment = attachment;
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
            _grenadeFuse.Deactivate();
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