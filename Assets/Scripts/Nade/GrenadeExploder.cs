using UnityEngine;

namespace Nade
{
    public class GrenadeExploder : MonoBehaviour
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