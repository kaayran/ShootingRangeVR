using Ammunition.CartridgeStructure;
using StructureComponents;
using UnityEngine;

namespace MagazineStructure
{
    internal class MagazineCartridgeExtractor : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;

        private Container<Cartridge, CartridgeType> _container;
        private Attachment _attachment;
        private Popper _popper;
        private MagazineAudio _magazineAudio;

        public void Init(Container<Cartridge, CartridgeType> container, Popper popper, Attachment attachment,
            MagazineAudio magazineAudio)
        {
            _magazineAudio = magazineAudio;
            _attachment = attachment;
            _container = container;
            _popper = popper;
            _popper.OnButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            if (!_container.TryPop(out var cartridge)) return;
            _attachment.TryGetHand(out var hand);

            var cartridgeTransform = cartridge.transform;
            cartridgeTransform.position = _extractTransform.position;
            cartridgeTransform.rotation = _extractTransform.rotation;

            var rb = cartridge.GetRigidbody();
            hand.GetEstimatedPeakVelocities(out var velocity, out var angularVelocity);
            rb.velocity = velocity;
            rb.angularVelocity = angularVelocity;

            _magazineAudio.PlayLoadBulletSound();

            cartridge.Activate();
        }
    }
}