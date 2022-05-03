using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosionView : MonoBehaviour
    {
        private GrenadeExplosion _explosion;

        public void Init(GrenadeExplosion grenadeExplosion)
        {
            _explosion = grenadeExplosion;
            _explosion.OnExplosion += Explosion;
        }

        private void Explosion()
        {
            var explosion = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            explosion.transform.position = transform.root.position;
            explosion.transform.rotation = transform.root.rotation;
            _explosion.OnExplosion -= Explosion;
        }
    }
}