using System;
using Ammo;
using UnityEngine;

namespace Weapon
{
    public class Chamber : MonoBehaviour
    {
        public event Action OnPatronEnter;
        public event Action OnPatronOut;

        private Patron _patron;

        public void Init()
        {
            _patron = null;
        }

        public bool TryPopPatron(out Patron patron)
        {
            patron = _patron;
            if (_patron == null) return false;

            _patron = null;
            OnPatronOut?.Invoke();

            return true;
        }

        public void TrySetPatron(Patron patron)
        {
            if (patron == null) return;
            if (_patron != null) return;

            _patron = patron;
            OnPatronEnter?.Invoke();
        }

        public bool CheckPatron()
        {
            return _patron != null;
        }
    }
}