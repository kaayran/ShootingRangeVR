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

        protected Rigidbody RigidBody;

        public abstract void Init();

        public void Activate()
        {
            RigidBody.velocity = Vector3.up;
            RigidBody.angularVelocity = Vector3.zero;

            gameObject.SetActive(true);
        }

        public void Deactivate() => gameObject.SetActive(false);
        public void DestroyCartridge() => Destroy(gameObject);
        public CartridgeType GetCartridgeType() => CartridgeType;
        public Attachment GetCartridgeAttachment() => CartridgeAttachment;
        public CartridgeBullet GetBullet() => CartridgeBullet;
        public CartridgeCase GetCartridge() => CartridgeCase;
        public Rigidbody GetRigidbody() => RigidBody;
        public Transform GetTransform() => transform;
        public float GetBulletSpeed() => CartridgeType.BulletSpeed;
    }
}