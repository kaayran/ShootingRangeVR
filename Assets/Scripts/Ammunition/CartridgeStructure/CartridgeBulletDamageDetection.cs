using Interfaces;
using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    public class CartridgeBulletDamageDetection : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (!TryGetComponent<IDamageable>(out var component)) return;

            var contactPoint = collision.GetContact(0).point;
            component.Damage(contactPoint);
            
            // For tests, Destroy() object after collision with object.
            Destroy(gameObject);
        }
    }
}