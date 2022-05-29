using Particle;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploderView : MonoBehaviour
    {
        [SerializeField] private ParticleComponent _particleComponent;

        private GrenadeFuseExploder _exploder;
        private GrenadeFuseAudio _fuseAudio;

        public void Init(GrenadeFuseExploder exploder, GrenadeFuseAudio fuseAudio)
        {
            _fuseAudio = fuseAudio;
            _exploder = exploder;
            _exploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            var transformSelf = transform;
            var particle = Instantiate(_particleComponent, transformSelf.position, transformSelf.rotation);
            particle.Play();
            
            _fuseAudio.PlayExplosionSound();

            _exploder.OnDetonate -= Detonate;
        }
    }
}