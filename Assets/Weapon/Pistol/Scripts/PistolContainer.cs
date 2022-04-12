using System;
using Round;
using UnityEngine;

namespace Weapon.Pistol.Scripts
{
    public class PistolContainer : Container<Magazine, MagazineType>
    {
        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private MagazineType _magazineType;

        private Magazine _magazine;

        public override void Init()
        {
        }

        public override bool TryPop(out Magazine magazine)
        {
            magazine = null;
            if (_magazine == null) return false;

            magazine = _magazine;
            _magazine = null;
            OnPopped?.Invoke();
            return true;
        }

        public override bool TryPush(Magazine magazine)
        {
            if (_magazine != null) return false;

            _magazine = magazine;
            OnEntered?.Invoke();
            return true;
        }

        public override Magazine GetStored()
        {
            return _magazine;
        }

        public override bool CheckStored()
        {
            throw new NotImplementedException();
        }

        public override MagazineType GetStoredType()
        {
            return (MagazineType) _magazineType.Clone();
        }
    }
}