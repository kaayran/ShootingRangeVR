using System;
using MagazineStructure;
using StructureComponents;
using UnityEngine;

namespace WeaponStructure
{
    public class WeaponContainer : Container<Magazine, MagazineType>
    {
        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private MagazineType magazineType;

        private Magazine _magazine;

        public override void Init()
        {
        }

        public override bool TryPop(out Magazine fuse)
        {
            fuse = null;
            if (_magazine == null) return false;

            fuse = _magazine;
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

        public override MagazineType GetStoredType()
        {
            return (MagazineType) magazineType.Clone();
        }
    }
}