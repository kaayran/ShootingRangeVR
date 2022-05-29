using System.Collections;
using UnityEngine;

namespace StructureComponents
{
    public class AudioOneShot : MonoBehaviour
    {
        private AudioSource _audioSource;
        private bool _isInit;
        private float _clipLength;

        public void Init(AudioClip clip, float volume, float pitch)
        {
            _audioSource = GetComponent<AudioSource>();

            _clipLength = clip.length;

            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
            
            _isInit = true;
        }

        private IEnumerator DestroySound(float time)
        {
            yield return new WaitForSeconds(time);

            Destroy(gameObject);
        }

        public void Play()
        {
            if (!_isInit) return;
            
            _audioSource.Play();
            
            StartCoroutine(DestroySound(_clipLength));
        }
    }
}