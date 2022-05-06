using Interfaces;
using Particle;
using Resources;
using UnityEngine;

namespace Detections
{
    [RequireComponent(typeof(Collider))]
    public class StaticHitDetection : MonoBehaviour, IDamageable
    {
        [SerializeField] private ParticleComponent _particle;

        public void Damage(DamageData damageData)
        {
            var particle = Instantiate(_particle, damageData.contactPoint, damageData.normalPoint);
            particle.Play();
        }
    }
}