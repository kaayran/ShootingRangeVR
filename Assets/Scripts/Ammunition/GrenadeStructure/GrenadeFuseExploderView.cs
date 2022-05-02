using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseExploderView : MonoBehaviour
    {
        private GrenadeFuseExploder _fuseExploder;

        public void Init(GrenadeFuseExploder exploder)
        {
            _fuseExploder = exploder;
            _fuseExploder.OnExplosion += OnExplosion;
        }

        private void OnExplosion()
        {
            InGameLogger.Log("Fuse explode", true);
        }
    }
}