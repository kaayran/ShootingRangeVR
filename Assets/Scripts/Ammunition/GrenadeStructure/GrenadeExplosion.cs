using System;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosion : MonoBehaviour
    {
        public event Action OnExplosion;

        private Container<GrenadeFuse, GrenadeFuseType> _container;
        private GrenadeFuseExploder _fuseExploder;
        private GrenadeFuse _fuse;

        public void Init(Container<GrenadeFuse, GrenadeFuseType> container)
        {
            _container = container;

            _container.OnEntered += Entered;
            _container.OnPopped += Popped;
        }

        private void Popped()
        {
            _fuse = null;
            _fuseExploder.OnDetonate -= Detonate;
            _fuseExploder = null;
        }

        private void Entered()
        {
            _fuse = _container.GetStored();
            _fuseExploder = _fuse.GetExploder();
            _fuseExploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            OnExplosion?.Invoke();

            _container.OnEntered -= Entered;
            _container.OnPopped -= Popped;

            _fuseExploder.OnDetonate -= Detonate;
            _fuseExploder = null;
        }
    }
}