using System;
using StructureComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannedFood
{
    public class CanCap : MonoBehaviour
    {
        [SerializeField] private int _takesToOpen;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioOneShot _audioOneShot;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<KnifeRazor>(out _)) return;

            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_open, 0.3f, Random.Range(0.9f, 1.1f));
            audioOneShot.Play();
            
            if (_takesToOpen-- == 1) return;

            Destroy(gameObject);
        }
    }
}