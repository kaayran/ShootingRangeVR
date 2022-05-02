using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeStriker : MonoBehaviour
    {
        private GrenadeFuse _grenadeFuse;

        public void Init()
        {
            _grenadeFuse = GetComponent<GrenadeFuse>();
            _grenadeFuse.OnDetonate += OnDetonate;
        }

        private void OnDetonate()
        {
            InGameLogger.Log("BOOM!", false);
            _grenadeFuse.OnDetonate -= OnDetonate;
        }
    }
}