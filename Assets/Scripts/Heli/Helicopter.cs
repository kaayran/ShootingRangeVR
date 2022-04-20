using System;
using Detections;
using UnityEngine;

namespace Heli
{
    public class Helicopter : MonoBehaviour
    {
        [SerializeField] private AudioSource _looped;
        [SerializeField] private AudioSource _message;
        [SerializeField] private BulletHitDetection _bulletHitDetection;
        [SerializeField] private HeliMove _heliMove;
        [SerializeField] private HeliVents _heliVents;
        private Rigidbody _rb;

        private void Start()
        {
            _looped.Play();
            _bulletHitDetection.OnDamage += HeliDown;
        }

        private void HeliDown()
        {
            _rb = gameObject.AddComponent<Rigidbody>();
            _message.Play();
            _looped.Stop();
            _rb.isKinematic = false;
            _rb.velocity += Vector3.forward * 5f;
            _rb.AddTorque(transform.up * 3f);
            
            Destroy(_heliMove);
            Destroy(_heliVents);

            _bulletHitDetection.OnDamage -= HeliDown;
        }
    }
}
