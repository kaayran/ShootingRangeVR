using Ammo;
using Interfaces;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Weapon;

namespace Round
{
    [RequireComponent(typeof(MagazineContainer))]
    [RequireComponent(typeof(MagazineExtractor))]
    [RequireComponent(typeof(Popper))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Throwable))]
    public abstract class Magazine : MonoBehaviour, IActivatable
    {
        [SerializeField] private protected MagazineInserter _magazineInserter;
        [SerializeField] private protected MagazineView _magazineView;
        [SerializeField] private protected MagazineType _magazineType;

        private protected MagazineContainer magazineContainer;
        private protected MagazineExtractor magazineExtractor;
        private protected Attachment magazineAttachment;
        private protected Popper magazinePopper;

        public abstract void Init();

        public bool TryPopPatron(out Patron patron)
        {
            if (magazineContainer.TryPop(out var poppedPatron))
            {
                patron = poppedPatron;
                return true;
            }

            patron = null;
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
            return (MagazineType) _magazineType.Clone();
        }

        public bool CheckPatron()
        {
            return magazineContainer.CheckStored();
        }

        internal Attachment GetMagazineAttachment()
        {
            return magazineAttachment;
        }
    }
}