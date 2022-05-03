using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploderView : MonoBehaviour
    {
        private GrenadeFuseExploder _exploder;

        public void Init(GrenadeFuseExploder exploder)
        {
            _exploder = exploder;
            _exploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            Destroy(gameObject);
            _exploder.OnDetonate -= Detonate;
        }
    }
}