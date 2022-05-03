using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeExplosionParticle : MonoBehaviour
    {
        private GrenadeExplosion _explosion;

        public void Init(GrenadeExplosion explosion)
        {
            _explosion = explosion;
            _explosion.OnExplosion += Explosion;
        }

        private void Explosion()
        {
            _explosion.OnExplosion -= Explosion;
        }
    }
}