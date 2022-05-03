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
            var particle = Instantiate(_particleComponent, transform.root.position, transform.root.rotation);
            particle.Play();
            
            _exploder.OnDetonate -= Detonate;
        }
    }
}