using System;
using System.Collections;
using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploder : MonoBehaviour
    {
        public event Action OnDetonate;

        [SerializeField] private float _radius;
        [SerializeField] private float _force;

        private GrenadeFuseStriker _fuseStriker;

        public void Init(GrenadeFuseStriker striker)
        {
            _fuseStriker = striker;
            _fuseStriker.OnStrike += Strike;
        }

        private void Strike(float delay)
        {
            StartCoroutine(ExplosionDelay(delay));
            _fuseStriker.OnStrike -= Strike;
        }

        private IEnumerator ExplosionDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            var hits = Physics.OverlapSphere(transform.position, _radius);

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.AddExplosionForce(_force, transform.position, _radius);
                }
            }
            
            OnDetonate?.Invoke();

            TryGetComponent<Attachment>(out var attachment);
            Destroy(attachment);
            Destroy(gameObject);
        }
    }
}