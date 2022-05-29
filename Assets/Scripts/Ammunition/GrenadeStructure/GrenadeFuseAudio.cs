using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private AudioClip _ring;


        private AudioSource _fuseSource;

        private void Start()
        {
            _fuseSource = GetComponent<AudioSource>();
        }
        
        public void PlayRingSound()
        {
            _fuseSource.volume = 0.25f;
            _fuseSource.clip = _ring;
            _fuseSource.pitch = Random.Range(0.925f, 1.2f);
            _fuseSource.Play();
        }

        public void PlayExplosionSound()
        {
            _fuseSource.volume = 0.2f;
            _fuseSource.clip = _explosion;
            _fuseSource.pitch = Random.Range(0.925f, 1.1f);
            _fuseSource.Play();
        }
    }
}