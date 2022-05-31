using System;
using StructureComponents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannedFood
{
    [RequireComponent(typeof(Rigidbody))]
    public class CanCap : MonoBehaviour
    {
        [SerializeField] private float _force;
        [SerializeField] private int _takesToOpen;
        [SerializeField] private AudioClip _open;
        [SerializeField] private AudioOneShot _audioOneShot;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<KnifeRazor>(out var razor)) return;

            var audioOneShot = Instantiate(_audioOneShot, transform.position, transform.rotation);
            audioOneShot.Init(_open, 0.3f, Random.Range(0.9f, 1.1f));
            audioOneShot.Play();
            
            if (_takesToOpen-- == 1) return;
            
            _rigidbody.AddForce(-razor.transform.forward * _force, ForceMode.Impulse);
            _rigidbody.AddTorque(-razor.transform.right * _force / 5, ForceMode.Impulse);
            
            Destroy(this);
        }
    }
}