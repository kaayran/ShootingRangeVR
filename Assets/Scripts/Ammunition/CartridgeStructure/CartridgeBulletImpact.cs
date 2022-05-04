using System;
using Interfaces;
using Resources;
using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    public class CartridgeBulletImpact : MonoBehaviour
    {
        public Action Hit;

        [SerializeField] private float _impactMultiplier;
        [SerializeField] private float _rayLength;

        private Rigidbody _rb;

        public void Init(Rigidbody rb)
        {
            enabled = false;
            _rb = rb;
        }

        private void Update()
        {
            if (!Physics.Raycast(transform.position, transform.forward * _rayLength, out var hit)) return;

            var obj = hit.transform.gameObject;

            if (!obj.TryGetComponent<IDamageable>(out var component)) return;

            var contactPoint = hit.point;
            var normal = hit.normal;
            var normalPoint = Quaternion.LookRotation(normal);

            var data = new DamageData
            {
                contactPoint = contactPoint,
                normalPoint = normalPoint,
                force = transform.forward * (_rb.velocity.magnitude * _rb.mass) / _impactMultiplier
            };

            component.Damage(data);

            Hit?.Invoke();
        }
    }
}