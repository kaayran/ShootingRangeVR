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
            OnDamage?.Invoke();
            var obj = Instantiate(_damageMark, contactPoint, normal);
            obj.transform.SetParent(transform, true);
        }
    }
}