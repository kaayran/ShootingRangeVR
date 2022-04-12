using System;
using UnityEngine;
using Weapon;

namespace Ammunition.Grenade
{
    public class GrenadeContainer : Container<Fuse, FuseType>
    {
        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private FuseType _fuseType;

        private Fuse _fuse;

        public override void Init()
        {
        }

        public override bool TryPop(out Fuse fuse)
        {
            fuse = null;
            if (_fuse == null) return false;

            fuse = _fuse;
            _fuse = null;
            OnPopped?.Invoke();
            return true;
        }

        public override bool TryPush(Fuse fuse)
        {
            if (_fuse != null) return false;

            _fuse = fuse;
            OnEntered?.Invoke();
            return true;
        }

        public override Fuse GetStored()
        {
            return _fuse;
        }

        public override bool CheckStored()
        {
            throw new NotImplementedException();
        }

        public override FuseType GetStoredType()
        {
            return (FuseType) _fuseType.Clone();
        }
    }
}