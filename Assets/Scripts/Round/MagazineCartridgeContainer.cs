using System;
using System.Collections.Generic;
using Ammunition;
using Ammunition.Cartridge;
using UnityEngine;
using Weapon;

namespace Round
{
    internal class MagazineCartridgeContainer : Container<Cartridge, CartridgeType>
    {
        public event Action<int> OnQuantityUpdate;

        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private CartridgeType _cartridgeType;
        [SerializeField] private Cartridge _cartridge;
        [SerializeField] private int _capacity;

        private Stack<Cartridge> _cartridges;

        public override void Init()
        {
            _cartridges = new Stack<Cartridge>();

            for (var i = 0; i < _capacity; i++)
            {
                var cartridge = Instantiate(_cartridge);
                cartridge.Init();
                cartridge.Deactivate();
                _cartridges.Push(cartridge);
            }
        }

        public override bool TryPop(out Cartridge cartridge)
        {
            cartridge = null;
            if (_cartridges.Count == 0) return false;

            cartridge = _cartridges.Pop();
            OnPopped?.Invoke();
            OnQuantityUpdate?.Invoke(_cartridges.Count);

            return true;
        }

        public override bool TryPush(Cartridge cartridge)
        {
            if (_cartridges.Count == _capacity) return false;

            _cartridges.Push(cartridge);
            OnEntered?.Invoke();
            OnQuantityUpdate?.Invoke(_cartridges.Count);

            return true;
        }

        public override Cartridge GetStored()
        {
            return _cartridge;
        }

        public override bool CheckStored()
        {
            return _cartridges.Count != 0;
        }

        public override CartridgeType GetStoredType()
        {
            return (CartridgeType) _cartridgeType.Clone();
        }

        public int GetQuantity()
        {
            return _cartridges.Count;
        }
    }
}