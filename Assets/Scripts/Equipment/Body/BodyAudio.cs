using UnityEngine;

namespace Equipment.Body
{
    [RequireComponent(typeof(AudioSource))]
    public class BodyAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip _equip;

        private AudioSource _armorSource;

        private void Start()
        {
            _armorSource = GetComponent<AudioSource>();
        }

        public void PlayEquipSound()
        {
            _armorSource.volume = 1f;
            _armorSource.clip = _equip;
            _armorSource.pitch = Random.Range(0.9f, 1.1f);
            _armorSource.Play();
        }

        public void PlayUnEquipSound()
        {
            _armorSource.volume = 0.75f;
            _armorSource.clip = _equip;
            _armorSource.pitch = Random.Range(0.9f, 1.1f);
            _armorSource.Play();
        }
    }
}