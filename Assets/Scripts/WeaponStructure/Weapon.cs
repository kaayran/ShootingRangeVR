using MagazineStructure;
using StructureComponents;
using UnityEngine;

namespace WeaponStructure
{
    public abstract class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponMagazineLoader WeaponMagazineLoader;
        [SerializeField] protected WeaponSlide WeaponSlide;

        protected WeaponMagazineExtractor WeaponMagazineExtractor;
        protected Container<Magazine, MagazineType> Container;
        protected WeaponChamberEjector WeaponChamberEjector;
        protected WeaponTrigger WeaponTrigger;
        protected WeaponBarrel WeaponBarrel;
        protected Attachment Attachment;
        protected WeaponFiringPin WeaponFiringPin;
        protected WeaponChamber WeaponChamber;
        protected Popper Popper;
        protected CollisionIgnoring CollisionIgnoring;
        public abstract void Init();
    }
}