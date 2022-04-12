using System;
using Ammo;
using UnityEngine;

namespace Weapon
{
    public class Barrel : MonoBehaviour
    {
        public event Action OnBulletFired;

        [SerializeField] private Transform _fireTransform;
        [SerializeField] private float _speed;
        
        public void Init()
        {
            
        }

        public void Fire(Patron patron)
        {
            var bullet = patron.GetBullet();
            var bulletTransform = bullet.GetTransform();
            bulletTransform.position = _fireTransform.position;
            bulletTransform.rotation = _fireTransform.rotation;

            bullet.Deploy(_speed);
            OnBulletFired?.Invoke();

            patron.DestroyPatron();
        }
    }
}