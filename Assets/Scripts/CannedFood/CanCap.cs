using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannedFood
{
    [RequireComponent(typeof(AudioSource))]
    public class CanCap : MonoBehaviour
    {
        [SerializeField] private int _takesToOpen = 3;
        [SerializeField] private AudioClip _open;

        private AudioSource _canSource;

        private void Start()
        {
            _canSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<KnifeRazor>(out _)) return;

            _canSource.clip = _open;
            _canSource.pitch = Random.Range(0.925f, 1.2f);
            _canSource.Play();
            
            if (_takesToOpen-- == 1) return;

            Destroy(gameObject);
        }
    }
}