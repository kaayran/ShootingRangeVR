using System;
using System.Collections;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploder : MonoBehaviour
    {
        public event Action OnExplosion;

        private GrenadeFuseStriker _fuseStriker;

        public void Init(GrenadeFuseStriker striker)
        {
            _fuseStriker = striker;
            _fuseStriker.OnStrike += OnStrike;
        }

        private void OnStrike(float delay)
        {
            StartCoroutine(ExplosionDelay(delay));
            _fuseStriker.OnStrike -= OnStrike;
        }

        private IEnumerator ExplosionDelay(float delay)
        {
            yield return new WaitForSeconds(delay);

            OnExplosion?.Invoke();
        }
    }
}