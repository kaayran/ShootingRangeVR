using Round;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Weapon.Pistol.Scripts
{
    [RequireComponent(typeof(ChamberEjector))]
    [RequireComponent(typeof(PistolContainer))]
    [RequireComponent(typeof(Attachment))]
    [RequireComponent(typeof(Extractor))]
    [RequireComponent(typeof(FiringPin))]
    [RequireComponent(typeof(Throwable))]
    [RequireComponent(typeof(Trigger))]
    [RequireComponent(typeof(Chamber))]
    [RequireComponent(typeof(Barrel))]
    [RequireComponent(typeof(Popper))]
    public class Pistol : Weapon
    {
        public override void Init()
        {
            container = GetComponent<Container<Magazine, MagazineType>>();
            chamberEjector = GetComponent<ChamberEjector>();
            attachment = GetComponent<Attachment>();
            extractor = GetComponent<Extractor>();
            firingPin = GetComponent<FiringPin>();
            trigger = GetComponent<Trigger>();
            chamber = GetComponent<Chamber>();
            barrel = GetComponent<Barrel>();
            popper = GetComponent<Popper>();

            attachment.Init();
            popper.Init(attachment);
            trigger.Init(attachment);

            container.Init();
            _loader.Init(container, attachment);
            extractor.Init(container, popper, attachment);

            chamber.Init();
            barrel.Init();
            _slide.Init(barrel);
            chamberEjector.Init(_slide, chamber);

            firingPin.Init(container, chamberEjector, chamber, trigger, barrel, _slide);
        }

        private void Awake()
        {
            Init();
        }
    }
}