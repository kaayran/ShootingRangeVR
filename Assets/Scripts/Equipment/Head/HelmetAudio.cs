using UnityEngine;

namespace Equipment.Head
{
    [RequireComponent(typeof(AudioSource))]
    public class HelmetAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _equip;
        [SerializeField] private AudioClip _visor;

        private AudioSource _helmetSource;

        private void Start()
        {
            _helmetSource = GetComponent<AudioSource>();
        }

        public void PlayEquipSound()
        {
            _helmetSource.volume = 1f;
            _helmetSource.clip = _equip;
            _helmetSource.pitch = Random.Range(0.9f, 1.1f);
            _helmetSource.Play();
        }
        
        public void PlayUnEquipSound()
        {
            _helmetSource.volume = 0.75f;
            _helmetSource.clip = _equip;
            _helmetSource.pitch = Random.Range(0.9f, 1.1f);
            _helmetSource.Play();
        }

        public void PlayHelmetVisorSound()
        {
            _helmetSource.volume = 1f;
            _helmetSource.clip = _visor;
            _helmetSource.pitch = Random.Range(0.9f, 1.1f);
            _helmetSource.Play();
        }
    }
}