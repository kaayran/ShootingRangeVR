using UnityEngine;

namespace Ammunition.Cartridge
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _force;
        private Rigidbody _rb;

        public void Init()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Deploy(float speed)
        {
            var bulletCollider = GetComponent<Collider>();

            bulletCollider.isTrigger = false;
            _rb.isKinematic = false;
            var bulletTransform = transform;
            bulletTransform.parent = null;

            var direction = bulletTransform.forward * _force * speed;
            gameObject.SetActive(true);
            _rb.AddForce(direction, ForceMode.Impulse);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}