using UnityEngine;

namespace MagazineStructure
{
    public class MagazineAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _loadBullet;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayLoadBulletSound()
        {
            _audioSource.clip = _loadBullet;
            _audioSource.pitch = Random.Range(0.925f, 1.2f);
            _audioSource.Play();
        }
    }
}