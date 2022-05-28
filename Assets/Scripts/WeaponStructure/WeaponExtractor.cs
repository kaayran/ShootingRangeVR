using MagazineStructure;
using StructureComponents;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponExtractor : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;

        private Container<Magazine, MagazineType> _container;
        private Attachment _attachment;
        private Popper _popper;
        private WeaponAudio _weaponAudio;

        public void Init(Container<Magazine, MagazineType> container, Popper popper, Attachment attachment,
            WeaponAudio weaponAudio)
        {
            _weaponAudio = weaponAudio;
            _container = container;
            _attachment = attachment;
            _popper = popper;
            _popper.OnButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            if (!_container.TryPop(out var magazine)) return;
            _attachment.TryGetHand(out var hand);

            var magazineTransform = magazine.transform;
            magazineTransform.position = _extractTransform.position;
            magazineTransform.rotation = _extractTransform.rotation;

            var rb = magazine.GetRigidbody();
            rb.velocity = hand.GetTrackedObjectVelocity();
            rb.angularVelocity = hand.GetTrackedObjectAngularVelocity();

            _weaponAudio.PlayUnloadMagazineSound();

            magazine.Activate();
        }
    }
}