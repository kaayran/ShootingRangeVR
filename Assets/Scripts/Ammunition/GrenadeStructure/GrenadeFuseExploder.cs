using System;
using System.Collections;
using Particle;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploder : MonoBehaviour
    {
        public event Action OnDetonate;
        
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

            OnDetonate?.Invoke();
        }
    }
}