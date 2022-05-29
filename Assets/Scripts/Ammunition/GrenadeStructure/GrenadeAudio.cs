using StructureComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ammunition.GrenadeStructure
{
    public class GrenadeAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _explosion;
        [SerializeField] private AudioClip _load;
        [SerializeField] private AudioOneShot _audioOneShot;
        
        public void PlayLoadSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_load, 0.15f, Random.Range(0.925f, 1.2f));
            audioOneShot.Play();
        }

        public void PlayExplosionSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_explosion, 1f, Random.Range(0.925f, 1.2f));
            audioOneShot.Play();
        }
    }
}