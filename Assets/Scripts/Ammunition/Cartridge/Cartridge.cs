using Interfaces;
using UnityEngine;
using Weapon;

namespace Ammunition.Cartridge
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Cartridge : MonoBehaviour, IActivatable
    {
        [SerializeField] protected CartridgeType _cartridgeType;
        [SerializeField] protected CartridgeCase _cartridgeCase;
        [SerializeField] protected Bullet _bullet;

        protected Attachment CartridgeAttachment;

        public abstract void Init();

        public void Activate()
        {
            var rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.up;
            rb.angularVelocity = Vector3.zero;

            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void DestroyPatron()
        {
            Destroy(gameObject);
        }

        public CartridgeType GetPatronType()
        {
            return _cartridgeType;
        }

        public Attachment GetPatronAttachment()
        {
            return CartridgeAttachment;
        }

        public Bullet GetBullet()
        {
            return _bullet;
        }

        public CartridgeCase GetCartridge()
        {
            return _cartridgeCase;
        }

        public Rigidbody GetRigidbody()
        {
            return GetComponent<Rigidbody>();
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}