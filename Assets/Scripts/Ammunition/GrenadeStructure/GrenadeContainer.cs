using System;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeContainer : Container<GrenadeFuse, GrenadeFuseType>
    {
        public override event Action OnPopped;
        public override event Action OnEntered;

        [SerializeField] private GrenadeFuseType grenadeFuseType;

        private GrenadeFuse _grenadeFuse;

        public override void Init()
        {
        }

        public override bool TryPop(out GrenadeFuse grenadeFuse)
        {
            grenadeFuse = null;
            if (_grenadeFuse is null) return false;

            grenadeFuse = _grenadeFuse;
            _grenadeFuse = null;
            OnPopped?.Invoke();
            return true;
        }

        public override bool TryPush(GrenadeFuse grenadeFuse)
        {
            if (!(_grenadeFuse is null)) return false;

            _grenadeFuse = grenadeFuse;
            OnEntered?.Invoke();
            return true;
        }

        public override GrenadeFuse GetStored()
        {
            return _grenadeFuse;
        }

        public override bool CheckStored()
        {
            throw new NotImplementedException();
        }

        public override GrenadeFuseType GetStoredType()
        {
            return (GrenadeFuseType) grenadeFuseType.Clone();
        }
    }
}