using MagazineStructure;
using StructureComponents;
using UnityEngine;
using UnityEngine.Serialization;

namespace WeaponStructure
{
    public abstract class Weapon : MonoBehaviour
    {
        [FormerlySerializedAs("WeaponMagazineLoader")] [SerializeField] protected WeaponLoader weaponLoader;
        [SerializeField] protected WeaponSlide WeaponSlide;

        protected WeaponExtractor WeaponExtractor;
        protected Container<Magazine, MagazineType> Container;
        protected WeaponChamberEjector WeaponChamberEjector;
        protected WeaponTrigger WeaponTrigger;
        protected WeaponBarrel WeaponBarrel;
        protected Attachment Attachment;
        protected WeaponFiringPin WeaponFiringPin;
        protected WeaponChamber WeaponChamber;
        protected Popper Popper;
        protected CollisionIgnoring CollisionIgnoring;
        protected WeaponAudio WeaponAudio;
    }
}