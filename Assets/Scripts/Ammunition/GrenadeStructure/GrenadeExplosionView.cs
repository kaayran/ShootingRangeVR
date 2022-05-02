using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosionView : MonoBehaviour
    {
        [SerializeField] private GameObject _explosionEffect;

        private GrenadeExplosion _explosion;

        public void Init(GrenadeExplosion grenadeExplosion)
        {
            _explosion = grenadeExplosion;
            _explosion.OnExplosion += Explosion;
        }

        private void Explosion()
        {
            Instantiate(_explosionEffect, transform.position, transform.rotation);
        }
    }
}