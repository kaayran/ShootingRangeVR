using System;
using System.Collections.Generic;
using Ammo;
using UnityEngine;
using Weapon;

namespace Round
{
    internal class MagazineContainer : Container<Patron, PatronType>
    {
        public event Action<int> OnQuantityUpdate;

        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private PatronType _patronType;
        [SerializeField] private Patron _patron;
        [SerializeField] private int _capacity;

        private Stack<Patron> _patrons;

        public override void Init()
        {
            _patrons = new Stack<Patron>();

            for (var i = 0; i < _capacity; i++)
            {
                var ptr = Instantiate(_patron);
                ptr.Init();
                ptr.Deactivate();
                _patrons.Push(ptr);
            }
        }

        public override bool TryPop(out Patron patron)
        {
            patron = null;
            if (_patrons.Count == 0) return false;

            patron = _patrons.Pop();
            OnPopped?.Invoke();
            OnQuantityUpdate?.Invoke(_patrons.Count);
            return true;
        }

        public override bool TryPush(Patron patron)
        {
            if (_patrons.Count == _capacity) return false;

            _patrons.Push(patron);
            OnEntered?.Invoke();
            OnQuantityUpdate?.Invoke(_patrons.Count);
            return true;
        }

        public override Patron GetStored()
        {
            return _patron;
        }

        public override bool CheckStored()
        {
            return _patrons.Count != 0;
        }

        public override PatronType GetStoredType()
        {
            return (PatronType) _patronType.Clone();
        }

        public int GetQuantity()
        {
            return _patrons.Count;
        }
    }
}