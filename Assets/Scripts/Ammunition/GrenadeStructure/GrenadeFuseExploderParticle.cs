using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploderParticle : MonoBehaviour
    {
        private GrenadeFuseExploder _exploder;

        public void Init(GrenadeFuseExploder exploder)
        {
            _exploder = exploder;
            _exploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            _exploder.OnDetonate -= Detonate;
        }
    }
}