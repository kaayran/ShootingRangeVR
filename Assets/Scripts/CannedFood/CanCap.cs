using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannedFood
{
    public class CanCap : MonoBehaviour
    {
        [SerializeField] private int _takesToOpen = 3;

        private AudioSource _canSource;

        private void Start()
        {
            _canSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<KnifeRazor>(out _)) return;
            if (_takesToOpen-- == 1) return;

            _canSource.pitch = Random.Range(0.925f, 1.2f);
            _canSource.Play();
            
            Destroy(gameObject);
        }
    }
}