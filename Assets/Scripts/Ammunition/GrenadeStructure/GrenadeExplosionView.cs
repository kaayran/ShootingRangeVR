using Particle;
using UnityEngine;
using UnityEngine.Rendering;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosionView : MonoBehaviour
    {
        [SerializeField] private ParticleComponent _particleComponent;
        [SerializeField] private Volume _volume;
        private GrenadeExplosion _explosion;
        private GrenadeAudio _grenadeAudio;

        public void Init(GrenadeExplosion grenadeExplosion, GrenadeAudio grenadeAudio)
        {
            _grenadeAudio = grenadeAudio;
            _explosion = grenadeExplosion;
            _explosion.OnExplosion += Explosion;
        }

        private void Explosion()
        {
            _grenadeAudio.PlayExplosionSound();
            
            var transformSelf = transform;
            var particle = Instantiate(_particleComponent, transformSelf.position, transformSelf.rotation);
            particle.Play();

            var volume = Instantiate(_volume, transformSelf.position, transformSelf.rotation);
            Destroy(volume.gameObject, 5f);
            
            _grenadeAudio.PlayExplosionSound();

            _explosion.OnExplosion -= Explosion;
        }
    }
}