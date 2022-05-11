using System;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeContainer : Container<GrenadeFuse, GrenadeFuseType>
    {
        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private GrenadeFuseType _fuseType;

        private GrenadeFuse _fuse;

        public override void Init()
        {
        }

        public override bool TryPop(out GrenadeFuse fuse)
        {
            fuse = null;
            if (_fuse is null) return false;

            fuse = _fuse;
            _fuse = null;
            OnPopped?.Invoke();
            return true;
        }

        public override bool TryPush(GrenadeFuse grenadeFuse)
        {
            if (!(_fuse is null)) return false;

            _fuse = grenadeFuse;
            OnEntered?.Invoke();
            return true;
        }

        public override GrenadeFuse GetStored() => _fuse;
        public override GrenadeFuseType GetStoredType() => (GrenadeFuseType) _fuseType.Clone();
    }
}