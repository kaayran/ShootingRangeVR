using System.Collections;
using UnityEngine;

namespace Particle
{
    public class ParticleComponent : MonoBehaviour
    {
        [SerializeField] private float _deleteAfterTime;

        private ParticleSystem[] _particles;
        private bool _isInit;

        private void Init()
        {
            _particles = GetComponentsInChildren<ParticleSystem>();
            _isInit = true;
        }

        private IEnumerator DestroyParticle(float time)
        {
            yield return new WaitForSeconds(time);

            Destroy(gameObject);
        }

        public void Play()
        {
            if (!_isInit) Init();

            foreach (var particle in _particles)
            {
                particle.Play(false);
            }

            StartCoroutine(DestroyParticle(_deleteAfterTime));
        }
    }
}