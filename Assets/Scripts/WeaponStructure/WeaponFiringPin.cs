using MagazineStructure;
using StructureComponents;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponFiringPin : MonoBehaviour
    {
        private Container<Magazine, MagazineType> _container;
        private WeaponChamberEjector _ejector;
        private WeaponChamber _weaponChamber;
        private WeaponTrigger _weaponTrigger;
        private WeaponBarrel _weaponBarrel;
        private WeaponSlide _weaponSlide;

        public void Init(Container<Magazine, MagazineType> container, WeaponChamberEjector ejector,
            WeaponChamber weaponChamber,
            WeaponTrigger weaponTrigger, WeaponBarrel weaponBarrel,
            WeaponSlide weaponSlide)
        {
            _weaponTrigger = weaponTrigger;
            _weaponBarrel = weaponBarrel;
            _weaponSlide = weaponSlide;
            _container = container;
            _ejector = ejector;
            _weaponChamber = weaponChamber;


            _weaponTrigger.OnTriggerPressed += WeaponTriggerPressed;

            _weaponSlide.OnForward += OnForward;
            _weaponSlide.OnBackward += OnBackward;
        }

        private void WeaponTriggerPressed()
        {
            if (!_weaponSlide.IsInPosition()) return;

            if (!_weaponChamber.TryPopCartridge(out var cartridge)) return;

            var crt = cartridge.GetCartridge();
            _ejector.SetCartridge(crt);

            _weaponBarrel.Fire(cartridge);
        }

        private void OnBackward()
        {
            var magazine = _container.GetStored();
            if (magazine == null) return;
            if (!magazine.CheckCartridge()) return;

            _weaponSlide.ForwardSlide();
        }

        private void OnForward()
        {
            // Check is cartridge in chamber -> then return
            if (_weaponChamber.CheckCartridge()) return;

            var magazine = _container.GetStored();

            if (magazine == null) return;

            // Try get cartridge from magazine
            if (magazine.TryPopCartridge(out var cartridge))
            {
                _weaponChamber.TrySetCartridge(cartridge);
            }
        }
    }
}