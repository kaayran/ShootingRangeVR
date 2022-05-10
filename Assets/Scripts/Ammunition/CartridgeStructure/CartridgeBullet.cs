using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CartridgeBullet : MonoBehaviour
    {
        private CartridgeBulletImpact _bulletImpact;
        private Rigidbody _rb;

        public void Init()
        {
            _rb = GetComponent<Rigidbody>();

            _bulletImpact = GetComponent<CartridgeBulletImpact>();
            _bulletImpact.Init(_rb);
            _bulletImpact.Hit += OnHit;
        }

        public void Deploy(float speed)
        {
            var bulletCollider = GetComponent<Collider>();

            bulletCollider.isTrigger = false;
            _rb.isKinematic = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            var bulletTransform = transform;
            bulletTransform.parent = null;

            var direction = bulletTransform.forward * speed;
            gameObject.SetActive(true);
            _rb.AddForce(direction, ForceMode.Impulse);

            _bulletImpact.enabled = true;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        private void OnHit()
        {
            Destroy(gameObject);
            _bulletImpact.Hit -= OnHit;
        }
    }
}