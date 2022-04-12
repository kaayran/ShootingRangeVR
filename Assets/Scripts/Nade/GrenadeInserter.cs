using UnityEngine;
using Weapon;

namespace Nade
{
    public class GrenadeInserter : MonoBehaviour
    {
        private GrenadeContainer _grenadeContainer;
        private Attachment _attachment;
        private Fuse _fuse;

        public void Init(GrenadeContainer grenadeContainer, Attachment attachment)
        {
            _grenadeContainer = grenadeContainer;
            _attachment = attachment;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_fuse != null) return;
            if (!other.TryGetComponent<Fuse>(out var fuse)) return;
            if (fuse.GetAttachment().TryGetHand(out var hand)) return;

            _fuse = fuse;

            var attachment = _fuse.GetAttachment();
            attachment.OnDrop += OnDrop;
        }

        private void OnDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var fuseReqs = _grenadeContainer.GetStoredType().FuseName;
            var fuseName = _fuse.GetFuseType().FuseName;
            if (!fuseReqs.Equals(fuseName)) return;

            if (!_grenadeContainer.TryPush(_fuse)) return;

            var attachment = _fuse.GetAttachment();
            attachment.OnDrop -= OnDrop;
            _fuse.Deactivate();
            _fuse = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_fuse == null) return;
            if (!other.TryGetComponent<Fuse>(out var magazine)) return;
            if (_fuse != magazine) return;

            var magazineAttachment = _fuse.GetAttachment();
            magazineAttachment.OnDrop -= OnDrop;
            _fuse = null;
        }
    }
}