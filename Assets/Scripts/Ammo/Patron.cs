using Interfaces;
using UnityEngine;
using Weapon;

namespace Ammo
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Patron : MonoBehaviour, IActivatable
    {
        [SerializeField] protected PatronType _patronType;
        [SerializeField] protected Cartridge _cartridge;
        [SerializeField] protected Bullet _bullet;

        protected Attachment patronAttachment;

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

        public PatronType GetPatronType()
        {
            return _patronType;
        }

        public Attachment GetPatronAttachment()
        {
            return patronAttachment;
        }

        public Bullet GetBullet()
        {
            return _bullet;
        }

        public Cartridge GetCartridge()
        {
            return _cartridge;
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