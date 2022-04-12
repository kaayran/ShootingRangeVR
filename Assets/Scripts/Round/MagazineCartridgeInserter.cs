using Ammunition;
using Ammunition.Cartridge;
using UnityEngine;
using Weapon;

namespace Round
{
    [RequireComponent(typeof(Collider))]
    internal class MagazineCartridgeInserter : MonoBehaviour
    {
        private Container<Cartridge, CartridgeType> _container;
        private Attachment _attachment;
        private Cartridge _cartridge;

        public void Init(Container<Cartridge, CartridgeType> container, Attachment attachment)
        {
            _container = container;
            _attachment = attachment;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_cartridge != null) return;
            if (!other.TryGetComponent<Cartridge>(out var cartridge)) return;
            if (!cartridge.GetPatronAttachment().TryGetHand(out var hand)) return;

            _cartridge = cartridge;

            var attachment = _cartridge.GetPatronAttachment();
            attachment.OnDrop += OnPatronDrop;
        }

        private void OnPatronDrop()
        {
            if (!_attachment.TryGetHand(out var hand)) return;

            var magazinePatronTypeID = _container.GetStoredType().CartridgeName;
            var patronTypeID = _cartridge.GetPatronType().CartridgeName;
            if (!magazinePatronTypeID.Equals(patronTypeID)) return;

            if (!_container.TryPush(_cartridge)) return;

            var attachment = _cartridge.GetPatronAttachment();
            attachment.OnDrop -= OnPatronDrop;
            _cartridge.Deactivate();
            _cartridge = null;
        }

        private void OnTriggerExit(Collider other)
        {
            if (_cartridge == null) return;

            if (!other.TryGetComponent<Cartridge>(out var patron)) return;
            if (_cartridge != patron) return;

            var attachment = _cartridge.GetPatronAttachment();
            attachment.OnDrop -= OnPatronDrop;
            _cartridge = null;
        }
    }
}