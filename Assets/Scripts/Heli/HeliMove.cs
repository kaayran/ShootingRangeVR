using System;
using UnityEngine;

namespace Heli
{
    public class HeliMove : MonoBehaviour
    {
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.Rotate(transform.up * _angularSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}