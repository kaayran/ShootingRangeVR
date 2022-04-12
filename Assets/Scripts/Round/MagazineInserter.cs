using Ammo;
using UnityEngine;
using Weapon;

namespace Round
{
    [RequireComponent(typeof(Collider))]
    internal class MagazineInserter : MonoBehaviour
    {
        private Container<Patron, PatronType> _container;
        private Attachment _attachment;
        private Patron _patron;

        public void Init(Container<Patron, PatronType> container, Attachment attachment)
        {
            _container = container;
            _attachment = attachment;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_patron != null) return;
            if (!other.TryGetComponent<Patron>(out var patron)) return;
            if (!patron.GetPatronAttachment().TryGetHand(out var hand)) return;

            _patron = patron;

            var attachment = _patron.GetPatronAttachment();
            attachment.OnDrop += OnPatronDrop;
        }

        private void OnPatronDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var magazinePatronTypeID = _container.GetStoredType().PatronName;
            var patronTypeID = _patron.GetPatronType().PatronName;
            if (!magazinePatronTypeID.Equals(patronTypeID)) return;

            if (!_container.TryPush(_patron)) return;
            
            var attachment = _patron.GetPatronAttachment();
            attachment.OnDrop -= OnPatronDrop;
            _patron.Deactivate();
            _patron = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_patron == null) return;

            if (!other.TryGetComponent<Patron>(out var patron)) return;
            if (_patron != patron) return;

            var attachment = _patron.GetPatronAttachment();
            attachment.OnDrop -= OnPatronDrop;
            _patron = null;
        }
    }
}