using Ammunition.CartridgeStructure;
using Interfaces;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace MagazineStructure
{
    [RequireComponent(typeof(MagazineCartridgeContainer))]
    [RequireComponent(typeof(MagazineCartridgeExtractor))]
    [RequireComponent(typeof(Popper))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Throwable))]
    public abstract class Magazine : MonoBehaviour, IActivatable
    {
        [SerializeField] private protected MagazineCartridgeInserter MagazineCartridgeInserter;
        [SerializeField] private protected MagazineView MagazineView;
        [SerializeField] private protected MagazineType MagazineType;

        private protected MagazineCartridgeContainer MagazineCartridgeContainer;
        private protected MagazineCartridgeExtractor MagazineCartridgeExtractor;
        private protected Attachment MagazineAttachment;
        private protected Popper MagazinePopper;
        private protected Rigidbody Rigidbody;

        public abstract void Init();

        public bool TryPopCartridge(out Cartridge cartridge)
        {
            if (MagazineCartridgeContainer.TryPop(out var poppedCartridge))
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
            Rigidbody.velocity = Vector3.down;
            Rigidbody.angularVelocity = Vector3.zero;

            gameObject.SetActive(false);
        }

        public MagazineType GetMagazineType()
        {
            return (MagazineType) MagazineType.Clone();
        }

        public bool CheckCartridge()
        {
            return MagazineCartridgeContainer.CheckStored();
        }

        internal Attachment GetMagazineAttachment()
        {
            return MagazineAttachment;
        }

        public Rigidbody GetRigidbody()
        {
            return Rigidbody;
        }
    }
}