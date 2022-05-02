using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExtractor : MonoBehaviour
    {
        [SerializeField] private Transform _extractTransform;

        private Container<GrenadeFuse, GrenadeFuseType> _container;
        private Attachment _attachment;
        private Popper _popper;
        private Collider _collider;

        public void Init(Container<GrenadeFuse, GrenadeFuseType> container, Attachment attachment, Popper popper,
            Collider collider)
        {
            _container = container;
            _attachment = attachment;
            _popper = popper;
            _collider = collider;

            _popper.OnButtonPressed += OnButtonPressed;
        }

        private void OnButtonPressed()
        {
            if (!_container.TryPop(out var fuse)) return;
            _attachment.TryGetHand(out var hand);

            var fuseTransform = fuse.transform;
            fuseTransform.parent = null;
            fuseTransform.position = _extractTransform.position;
            fuseTransform.rotation = _extractTransform.rotation;

            var fuseColliders = fuseTransform.GetComponentsInChildren<Collider>();
            foreach (var fuseCollider in fuseColliders)
            {
                Physics.IgnoreCollision(_collider, fuseCollider, false);
            }

            var rb = fuse.GetRigidbody();
            rb.isKinematic = false;
            rb.velocity = hand.GetTrackedObjectVelocity();
            rb.angularVelocity = hand.GetTrackedObjectAngularVelocity();

            fuse.Activate();
        }
    }
}