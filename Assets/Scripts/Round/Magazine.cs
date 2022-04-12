using Ammunition;
using Ammunition.Cartridge;
using Interfaces;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Weapon;

namespace Round
{
    [RequireComponent(typeof(MagazineCartridgeContainer))]
    [RequireComponent(typeof(MagazineCartridgeExtractor))]
    [RequireComponent(typeof(Popper))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Throwable))]
    public abstract class Magazine : MonoBehaviour, IActivatable
    {
        [SerializeField] private protected MagazineCartridgeInserter magazineCartridgeInserter;
        [SerializeField] private protected MagazineView magazineView;
        [SerializeField] private protected MagazineType magazineType;

        private protected MagazineCartridgeContainer magazineCartridgeContainer;
        private protected MagazineCartridgeExtractor magazineCartridgeExtractor;
        private protected Attachment magazineAttachment;
        private protected Popper magazinePopper;

        public abstract void Init();

        public bool TryPopPatron(out Cartridge cartridge)
        {
            if (magazineCartridgeContainer.TryPop(out var poppedCartridge))
            {
                cartridge = poppedCartridge;
                return true;
            }

            cartridge = null;
            return false;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            var rb = GetComponent<Rigidbody>();
            rb.velocity = Vector3.down;
            rb.angularVelocity = Vector3.zero;

            gameObject.SetActive(false);
        }

        public MagazineType GetMagazineType()
        {
            return (MagazineType) magazineType.Clone();
        }

        public bool CheckPatron()
        {
            return magazineCartridgeContainer.CheckStored();
        }

        internal Attachment GetMagazineAttachment()
        {
            return magazineAttachment;
        }
    }
}