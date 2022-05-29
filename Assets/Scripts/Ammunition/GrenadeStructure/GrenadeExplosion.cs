using System;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosion : MonoBehaviour
    {
        public event Action OnExplosion;

        [SerializeField] private float _radius;
        [SerializeField] private float _force;

        private Container<GrenadeFuse, GrenadeFuseType> _container;
        private GrenadeFuseExploder _exploder;
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
            _exploder.OnDetonate -= Detonate;
            _exploder = null;
        }

        private void Entered()
        {
            _fuse = _container.GetStored();
            _exploder = _fuse.GetExploder();
            _exploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            var hits = Physics.OverlapSphere(transform.position, _radius);

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                }
            }
            
            OnExplosion?.Invoke();

            _container.OnEntered -= Entered;
            _container.OnPopped -= Popped;

            _exploder.OnDetonate -= Detonate;
            _exploder = null;

            Destroy(gameObject);
        }
    }
}