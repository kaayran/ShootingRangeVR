using System;
using Interfaces;
using UnityEngine;

namespace Detections
{
    public class BulletHitDetection : MonoBehaviour, IDamageable
    {
        [SerializeField] private GameObject _damageMark;
        public void Damage(Vector3 contactPoint, Quaternion normal)
        {
            Debug.Log("IDamageable!");
            Instantiate(_damageMark, contactPoint, normal);
        }
    }
}