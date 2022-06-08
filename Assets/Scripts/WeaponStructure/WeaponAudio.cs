using StructureComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeaponStructure
{
    public class WeaponAudio : MonoBehaviour
    {
        [SerializeField] private AudioOneShot _audioOneShot;
        [SerializeField] private AudioClip _shot;
        [SerializeField] private AudioClip _loadMag;
        [SerializeField] private AudioClip _unloadMag;
        [SerializeField] private AudioClip _backSlide;
        [SerializeField] private AudioClip _forwardSlide;

        public void PlayShotSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform);
            audioOneShot.Init(_shot, 0.9f, Random.Range(0.9f, 1.2f));
            audioOneShot.Play();
        }

        public void PlayLoadMagazineSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform);
            audioOneShot.Init(_loadMag, 0.25f, Random.Range(0.9f, 1.2f));
            audioOneShot.Play();
        }

        public void PlayUnloadMagazineSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform);
            audioOneShot.Init(_unloadMag, 0.25f, Random.Range(0.9f, 1.2f));
            audioOneShot.Play();
        }

        public void PlayBackwardSlideSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform);
            audioOneShot.Init(_backSlide, 0.15f, Random.Range(0.9f, 1.2f));
            audioOneShot.Play();
        }

        public void PlayForwardSlideSound()
        {
            var audioOneShot = Instantiate(_audioOneShot, transform);
            audioOneShot.Init(_forwardSlide, 0.15f, Random.Range(0.9f, 1.2f));
            audioOneShot.Play();
        }
    }
}