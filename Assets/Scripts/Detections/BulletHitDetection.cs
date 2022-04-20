using System;
using Interfaces;
using UnityEngine;

namespace Detections
{
    public class BulletHitDetection : MonoBehaviour, IDamageable
    {
        public event Action OnDamage;
        [SerializeField] private GameObject _damageMark;
        
        public void Damage(Vector3 contactPoint, Quaternion normal)
        {
            Debug.Log("IDamageable!");
            OnDamage?.Invoke();
            Instantiate(_damageMark, contactPoint, normal, transform);
        }
    }
}