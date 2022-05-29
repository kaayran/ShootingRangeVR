using StructureComponents;
using UnityEngine;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeFuseAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private AudioClip _ring;
        [SerializeField] private AudioOneShot _audioOneShot;
        
        public void PlayRingSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_ring, 0.2f, Random.Range(0.925f, 1.2f));
            audioOneShot.Play();
        }

        public void PlayExplosionSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_explosion, 0.1f, Random.Range(0.925f, 1.2f));
            audioOneShot.Play();
        }
    }
}