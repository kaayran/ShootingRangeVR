using UnityEngine;
using Random = UnityEngine.Random;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private AudioClip _load;

        private AudioSource _grenadeSource;

        private void Start()
        {
            _grenadeSource = GetComponent<AudioSource>();
        }

        public void PlayExplosionSound()
        {
            _grenadeSource.clip = _explosion;
            _grenadeSource.pitch = Random.Range(0.925f, 1.2f);
            _grenadeSource.Play();
        }

        public void PlayLoadSound()
        {
            _grenadeSource.clip = _load;
            _grenadeSource.pitch = Random.Range(0.925f, 1.2f);
            _grenadeSource.Play();
        }
    }
}