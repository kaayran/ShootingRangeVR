using System;
using Ammunition.CartridgeStructure;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponChamber : MonoBehaviour
    {
        public event Action OnCartridgeEnter;
        public event Action OnCartridgeOut;

        private Cartridge _cartridge;

        public void Init()
        {
            _cartridge = null;
        }

        public bool TryPopCartridge(out Cartridge cartridge)
        {
            cartridge = _cartridge;
            if (_cartridge == null) return false;

            _cartridge = null;
            OnCartridgeOut?.Invoke();

            return true;
        }

        public void TrySetCartridge(Cartridge cartridge)
        {
            if (cartridge == null) return;
            if (_cartridge != null) return;

            _cartridge = cartridge;
            OnCartridgeEnter?.Invoke();
        }

        public bool CheckCartridge()
        {
            return _cartridge != null;
        }
    }
}