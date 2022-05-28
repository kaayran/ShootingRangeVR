using MagazineStructure;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;
using WeaponStructure;

namespace WeaponsRealization
{
    [RequireComponent(typeof(WeaponContainer))]
    [RequireComponent(typeof(WeaponAudio))]
    [RequireComponent(typeof(WeaponExtractor))]
    [RequireComponent(typeof(WeaponChamberEjector))]
    [RequireComponent(typeof(CollisionIgnoring))]
    [RequireComponent(typeof(WeaponFiringPin))]
    [RequireComponent(typeof(WeaponChamber))]
    [RequireComponent(typeof(WeaponTrigger))]
    [RequireComponent(typeof(WeaponBarrel))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Throwable))]
    [RequireComponent(typeof(Popper))]
    public class Pistol : Weapon
    {
        public virtual void Init()
        {
            WeaponExtractor = GetComponent<WeaponExtractor>();
            Container = GetComponent<Container<Magazine, MagazineType>>();
            WeaponChamberEjector = GetComponent<WeaponChamberEjector>();
            CollisionIgnoring = GetComponent<CollisionIgnoring>();
            WeaponFiringPin = GetComponent<WeaponFiringPin>();
            WeaponTrigger = GetComponent<WeaponTrigger>();
            WeaponChamber = GetComponent<WeaponChamber>();
            WeaponBarrel = GetComponent<WeaponBarrel>();
            Attachment = GetComponent<Attachment>();
            Popper = GetComponent<Popper>();
            WeaponAudio = GetComponent<WeaponAudio>();

            CollisionIgnoring.Init();
            Attachment.Init();
            Popper.Init(Attachment);
            WeaponTrigger.Init(Attachment);

            Container.Init();
            weaponLoader.Init(Container, Attachment, WeaponAudio);
            WeaponExtractor.Init(Container, Popper, Attachment, WeaponAudio);

            WeaponChamber.Init();
            WeaponBarrel.Init(WeaponAudio);
            WeaponSlide.Init(WeaponBarrel, WeaponAudio);
            WeaponChamberEjector.Init(WeaponSlide, WeaponChamber);

            WeaponFiringPin.Init(Container, WeaponChamberEjector, WeaponChamber, WeaponTrigger, WeaponBarrel,
                WeaponSlide);
        }

        private void Awake()
        {
            Init();
        }
    }
}