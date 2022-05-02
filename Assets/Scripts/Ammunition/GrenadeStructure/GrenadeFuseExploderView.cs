using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploderView : MonoBehaviour
    {
        [SerializeField] private GameObject _explosion;
        private GrenadeFuseExploder _fuseExploder;

        public void Init(GrenadeFuseExploder exploder)
        {
            _fuseExploder = exploder;
            _fuseExploder.OnDetonate += Detonate;
        }

        private void Detonate()
        {
            Instantiate(_explosion, transform.position, transform.rotation);
        }
    }
}