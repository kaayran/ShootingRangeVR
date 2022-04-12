using Interfaces;
using StructureComponents;
using UnityEngine;

namespace Ammunition.CartridgeStructure
{
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class Cartridge : MonoBehaviour, IActivatable
    {
        [SerializeField] protected CartridgeBullet CartridgeBullet;
        [SerializeField] protected CartridgeType CartridgeType;
        [SerializeField] protected CartridgeCase CartridgeCase;

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

        public void DestroyCartridge()
        {
            Destroy(gameObject);
        }

        public CartridgeType GetCartridgeType()
        {
            return CartridgeType;
        }

        public Attachment GetCartridgeAttachment()
        {
            return CartridgeAttachment;
        }

        public CartridgeBullet GetBullet()
        {
            return CartridgeBullet;
        }

        public CartridgeCase GetCartridge()
        {
            return CartridgeCase;
        }

        public Rigidbody GetRigidbody()
        {
            return GetComponent<Rigidbody>();
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public float GetBulletSpeed()
        {
            return CartridgeType.BulletSpeed;
        }
    }
}