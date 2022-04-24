using MagazineStructure;
using StructureComponents;
using UnityEngine;
using Valve.VR.InteractionSystem;
using WeaponStructure;

namespace WeaponsRealization
{
    [RequireComponent(typeof(WeaponMagazineContainer))]
    [RequireComponent(typeof(WeaponMagazineExtractor))]
    [RequireComponent(typeof(WeaponChamberEjector))]
    [RequireComponent(typeof(CollisionIgnoring))]
    [RequireComponent(typeof(WeaponFiringPin))]
    [RequireComponent(typeof(WeaponChamber))]
    [RequireComponent(typeof(WeaponTrigger))]
    [RequireComponent(typeof(WeaponBarrel))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Throwable))]
    [RequireComponent(typeof(Popper))]
    public class Pistol : WeaponStructure.Weapon
    {
        public override void Init()
        {
            WeaponMagazineExtractor = GetComponent<WeaponMagazineExtractor>();
            Container = GetComponent<Container<Magazine, MagazineType>>();
            WeaponChamberEjector = GetComponent<WeaponChamberEjector>();
            CollisionIgnoring = GetComponent<CollisionIgnoring>();
            WeaponFiringPin = GetComponent<WeaponFiringPin>();
            WeaponTrigger = GetComponent<WeaponTrigger>();
            WeaponChamber = GetComponent<WeaponChamber>();
            WeaponBarrel = GetComponent<WeaponBarrel>();
            Attachment = GetComponent<Attachment>();
            Popper = GetComponent<Popper>();

            CollisionIgnoring.Init();
            Attachment.Init();
            Popper.Init(Attachment);
            WeaponTrigger.Init(Attachment);

            Container.Init();
            WeaponMagazineLoader.Init(Container, Attachment);
            WeaponMagazineExtractor.Init(Container, Popper, Attachment);

            WeaponChamber.Init();
            WeaponBarrel.Init();
            WeaponSlide.Init(WeaponBarrel);
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