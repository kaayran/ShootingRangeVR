using Particle;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploderView : MonoBehaviour
    {        
        [SerializeField] private ParticleComponent _particleComponent;

        private GrenadeFuseExploder _exploder;

        public void Init(GrenadeFuseExploder exploder)
        {
            _exploder = exploder;
            _exploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            var transformSelf = transform;
            var particle = Instantiate(_particleComponent, transformSelf.position, transformSelf.rotation);
            particle.Play();
            
            _exploder.OnDetonate -= Detonate;
        }
    }
}