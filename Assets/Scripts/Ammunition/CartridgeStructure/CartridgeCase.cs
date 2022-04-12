using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CartridgeCase : MonoBehaviour
    {
        private Rigidbody _rb;

        public void Init()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public Rigidbody GetRigidbody()
        {
            return _rb;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public void Activate()
        {
            var bulletCollider = GetComponent<Collider>();
            bulletCollider.isTrigger = false;
            _rb.isKinematic = false;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            transform.parent = null;
        }
    }
}