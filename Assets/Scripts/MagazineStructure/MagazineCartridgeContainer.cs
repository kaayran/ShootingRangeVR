using System;
using System.Collections.Generic;
using Ammunition.CartridgeStructure;
using StructureComponents;
using UnityEngine;

namespace MagazineStructure
{
    internal class MagazineCartridgeContainer : Container<Cartridge, CartridgeType>
    {
        public event Action<int> OnQuantityUpdate;
        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private bool _isAutoLoaded;
        [SerializeField] private Cartridge _cartridge;
        [SerializeField] private int _capacity;
        [SerializeField] private string _caliber;

        private Stack<Cartridge> _cartridges;

        public override void Init()
        {
            _cartridges = new Stack<Cartridge>();

            if (!_isAutoLoaded) return;
            
            for (var i = 0; i < _capacity; i++)
            {
                var cartridge = Instantiate(_cartridge);
                cartridge.Init();
                cartridge.Deactivate();
                _cartridges.Push(cartridge);
            }
        }

        public override bool TryPop(out Cartridge fuse)
        {
            fuse = null;
            if (_cartridges.Count == 0) return false;

            fuse = _cartridges.Pop();
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

        public virtual bool CheckStored()
        {
            return _cartridges.Count != 0;
        }

        // Need to do something with this shit.
        public override CartridgeType GetStoredType()
        {
            return ScriptableObject.CreateInstance<CartridgeType>();
        }

        public int GetQuantity()
        {
            return _cartridges.Count;
        }

        public string GetCaliber()
        {
            return _caliber;
        }
    }
}