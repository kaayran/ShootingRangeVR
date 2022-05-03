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
            var explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            explosion.transform.localScale = Vector3.one * 0.25f;
            explosion.transform.position = transform.root.position;
            explosion.transform.rotation = transform.root.rotation;
            _exploder.OnDetonate -= Detonate;
        }
    }
}