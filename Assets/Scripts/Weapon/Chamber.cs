using System;
using Ammunition;
using Ammunition.Cartridge;
using UnityEngine;

namespace Weapon
{
    public class Chamber : MonoBehaviour
    {
        public event Action OnPatronEnter;
        public event Action OnPatronOut;

        private Cartridge _cartridge;

        public void Init()
        {
            _cartridge = null;
        }

        public bool TryPopPatron(out Cartridge cartridge)
        {
            cartridge = _cartridge;
            if (_cartridge == null) return false;

            _cartridge = null;
            OnPatronOut?.Invoke();

            return true;
        }

        public void TrySetPatron(Cartridge cartridge)
        {
            if (cartridge == null) return;
            if (_cartridge != null) return;

            _cartridge = cartridge;
            OnPatronEnter?.Invoke();
        }

        public bool CheckPatron()
        {
            return _cartridge != null;
        }
    }
}