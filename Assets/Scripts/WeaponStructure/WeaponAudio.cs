using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WeaponStructure
{
    public class WeaponAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _shot;
        [SerializeField] private AudioClip _loadMag;
        [SerializeField] private AudioClip _unloadMag;
        [SerializeField] private AudioClip _backSlide;
        [SerializeField] private AudioClip _forwardSlide;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayShotSound()
        {
            _audioSource.clip = _shot;
            _audioSource.pitch = Random.Range(0.925f, 1.2f);
            _audioSource.Play();
        }

        public void PlayLoadMagazineSound()
        {
            _audioSource.clip = _loadMag;
            _audioSource.pitch = Random.Range(0.925f, 1.2f);
            _audioSource.Play();
        }

        public void PlayUnloadMagazineSound()
        {
            _audioSource.clip = _unloadMag;
            _audioSource.pitch = Random.Range(0.925f, 1.2f);
            _audioSource.Play();
        }

        public void PlayBackwardSlideSound()
        {
            _audioSource.clip = _backSlide;
            _audioSource.pitch = Random.Range(0.925f, 1.2f);
            _audioSource.Play();
        }

        public void PlayForwardSlideSound()
        {
            _audioSource.clip = _forwardSlide;
            _audioSource.pitch = Random.Range(0.925f, 1.2f);
            _audioSource.Play();
        }
    }
}