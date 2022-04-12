using UnityEngine;

namespace Ammunition.Grenade
{
    public class Striker : MonoBehaviour
    {
        [SerializeField] private Fuse _fuse;

        public void Init()
        {
            _fuse.OnDetonate += OnDetonate;
        }

        private void OnDetonate()
        {
            Debug.Log("BOOM!");
        }
    }
}