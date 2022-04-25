using System;
using Interfaces;
using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class CartridgeBullet : MonoBehaviour
    {
        [SerializeField] private float _impactMultiplier;
        [SerializeField] private float _force;
        [SerializeField] private float _rayLength;

        private Rigidbody _rb;
        private bool _isDeployed;

        public void Init()
        {
            _rb = GetComponent<Rigidbody>();

            _isDeployed = false;
        }

        public void Deploy(float speed)
        {
            var bulletCollider = GetComponent<Collider>();

            bulletCollider.isTrigger = false;
            _rb.isKinematic = false;
            _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            var bulletTransform = transform;
            bulletTransform.parent = null;

            var direction = bulletTransform.forward * _force * speed;
            gameObject.SetActive(true);
            _rb.AddForce(direction, ForceMode.Impulse);

            _isDeployed = true;
        }

        public Transform GetTransform()
        {
            return transform;
        }

        private void Update()
        {
            if (!_isDeployed) return;
            if (!Physics.Raycast(transform.position, transform.forward * _rayLength, out var hit)) return;

            var obj = hit.transform.gameObject;
            var hitPoint = hit.point;
            Debug.Log($"Raycast hit: {obj.name}");

            if (hit.transform.gameObject.TryGetComponent<IDamageable>(out var component))
            {
                var hitNormal = hit.normal;
                var normal = Quaternion.LookRotation(hitNormal);

                component.Damage(hitPoint, normal);
            }

            if (!obj.TryGetComponent<Rigidbody>(out var rb))
            {
                Destroy(gameObject);
                return;
            }
            
            var bulletForce = transform.forward * _rb.velocity.magnitude * _rb.mass / _impactMultiplier;
            rb.AddForceAtPosition(bulletForce, hitPoint, ForceMode.Impulse);
            Debug.Log($"Bullet force: {bulletForce}");

            Destroy(gameObject);
        }
    }
}