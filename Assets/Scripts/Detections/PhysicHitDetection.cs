using Interfaces;
using Resources;
using UnityEngine;

namespace Detections
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class PhysicHitDetection : MonoBehaviour, IDamageable
    {
        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Damage(DamageData damageData)
        {
            var force = damageData.force;
            var contactPoint = damageData.contactPoint;

            _rb.AddForceAtPosition(force, contactPoint, ForceMode.Impulse);
        }
    }
}