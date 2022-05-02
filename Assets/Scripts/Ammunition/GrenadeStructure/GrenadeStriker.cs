using UnityEngine;
using Utilities.Logger;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeStriker : MonoBehaviour
    {
        [SerializeField] private GrenadeFuse _grenadeFuse;

        public void Init()
        {
            _grenadeFuse.Detonate += OnDetonate;
        }

        private void OnDetonate()
        {
            InGameLogger.Log("BOOM!", false);
            _grenadeFuse.Detonate -= OnDetonate;
        }
    }
}