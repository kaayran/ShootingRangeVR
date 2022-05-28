using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _explosion;


        private AudioSource _fuseSource;

        private void Start()
        {
            _fuseSource = GetComponent<AudioSource>();
        }

        public void PlayExplosionSound()
        {
            _fuseSource.volume = 0.1f;
            _fuseSource.clip = _explosion;
            _fuseSource.pitch = Random.Range(0.925f, 1.2f);
            _fuseSource.Play();
        }
    }
}