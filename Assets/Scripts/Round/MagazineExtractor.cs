using Ammo;
using UnityEngine;
using Weapon;

namespace Round
{
    internal class MagazineExtractor : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;

        private Container<Patron, PatronType> _container;
        private Attachment _attachment;
        private Popper _popper;

        public void Init(Container<Patron, PatronType> container, Popper popper, Attachment attachment)
        {
            _attachment = attachment;
            _container = container;
            _popper = popper;
            _popper.OnButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            if (!_container.TryPop(out var patron)) return;
            _attachment.TryGetHand(out var hand);

            var patronTransform = patron.transform;
            patronTransform.position = _extractTransform.position;
            patronTransform.rotation = _extractTransform.rotation;

            var rb = patron.GetComponent<Rigidbody>();
            rb.velocity = hand.GetTrackedObjectVelocity();
            rb.angularVelocity = hand.GetTrackedObjectAngularVelocity();

            patron.Activate();
        }
    }
}