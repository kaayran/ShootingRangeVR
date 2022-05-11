using Particle;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosionView : MonoBehaviour
    {
        [SerializeField] private ParticleComponent _particleComponent;

        private GrenadeExplosion _explosion;

        public void Init(GrenadeExplosion grenadeExplosion)
        {
            _explosion = grenadeExplosion;
            _explosion.OnExplosion += Explosion;
        }

        private void Explosion()
        {
            var transformSelf = transform;
            var particle = Instantiate(_particleComponent, transformSelf.position, transformSelf.rotation);
            particle.Play();

            _explosion.OnExplosion -= Explosion;
        }
    }
}