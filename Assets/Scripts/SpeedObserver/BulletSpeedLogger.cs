using System;
using UnityEngine;

namespace SpeedObserver
{
    public class BulletSpeedLogger : MonoBehaviour
    {
        private Rigidbody _bulletRigidbody;

        private void Start()
        {
            _bulletRigidbody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            Debug.Log(_bulletRigidbody.velocity);
        }
    }
}