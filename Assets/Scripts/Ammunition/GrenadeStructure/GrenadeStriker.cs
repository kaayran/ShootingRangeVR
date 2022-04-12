using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeStriker : MonoBehaviour
    {
        [SerializeField] private GrenadeFuse grenadeFuse;

        public void Init()
        {
            grenadeFuse.OnDetonate += OnDetonate;
        }

        private void OnDetonate()
        {
            Debug.Log("BOOM!");
        }
    }
}